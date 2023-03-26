using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.DataAccess;

namespace DataAccess
{
    public class ChatDAO
    {
        public static async Task CreateChat(Chat chat)
        {
            using (var db = new AMSContext())
            {
                db.Chats.Add(chat);
                await db.SaveChangesAsync();
            }
        }

        public static void SaveMessage(int groupid, int senderId, List<int> receiverIs, string message, string? filePath)
        {
            // Save message to database here
            using (var db = new AMSContext())
            {
                var chat = new Chat
                {
                    GroupId = groupid,
                    SenderId = senderId,
                    Message = message,
                    FilePath = filePath,
                    SentDate = DateTime.Now
                };
                db.Chats.Add(chat);
                db.SaveChanges();
            }
        }
    }
}
