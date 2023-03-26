using System.Text.RegularExpressions;
using Microsoft.AspNetCore.SignalR;

namespace AMSClient.SignalRChat.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string groupName, string user, string message)
        {
            var groupId = groupName.Substring(groupName.IndexOf("-c") + 2);
            // check if message  match regex "/dcdscc/" 2 times or more times
            var match = Regex.Match(message, @"\/\w+\/");
            if (match.Success)
            {
                // get the file name
                var fileName = message.Substring(message.LastIndexOf("/") + 1);
                // send the file to the client
                await Clients.Group(groupName).SendAsync("ReceiveFile", user, groupId, fileName, message);
            }else{
                // send the message to the client
                await Clients.Group(groupName).SendAsync("ReceiveMessage", user, groupId, message);
            }
        }

        public async Task JoinGroup(string groupName, string userName)
        {
            var groupId = groupName.Substring(groupName.IndexOf("-c") + 2);
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            await Clients.Group(groupName).SendAsync("UserJoined", userName, groupId);
        }

        public async Task LeaveGroup(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
            await Clients.Group(groupName).SendAsync("UserLeft", Context.User.Identity.Name);
        }
    }
}
