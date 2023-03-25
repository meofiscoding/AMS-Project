using Microsoft.AspNetCore.SignalR;

namespace AMSClient.SignalRChat.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string groupName, string user, string? message = "", IFormFile? file = null)
        {
            //extract groupid from pattern group+"-c"+groupId
            var groupId = groupName.Substring(groupName.IndexOf("-c") + 2);
            if (file != null)
            {
                // Create a unique filename for the file
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                // Save the file to a local folder
                var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\uploads");

                // Create directory if it doesn't exist
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }
                var filePath = Path.Combine(directoryPath, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
                await Clients.Group(groupName).SendAsync("ReceiveFile", user, groupId, message,file.FileName);
            }
            else
            {
                // Save the message to the database
                // ...

                // Send the message to all clients in the group
                await Clients.Group(groupName).SendAsync("ReceiveMessage", user, groupId, message, file);
            }
            // Send the message to all clients in the group
        }

        public async Task SendFile(string groupName, string user, byte[] fileData, string fileName)
        {
            // Save the file to the database
            // ...

            // Send the file to all clients in the group
            await Clients.Group(groupName).SendAsync("ReceiveFile", user, fileData, fileName);
        }

        public async Task JoinGroup(string groupName, string userName)
        {
            try
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
                await Clients.Group(groupName).SendAsync("UserJoined", userName);
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task LeaveGroup(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
            await Clients.Group(groupName).SendAsync("UserLeft", Context.User.Identity.Name);
        }
    }
}
