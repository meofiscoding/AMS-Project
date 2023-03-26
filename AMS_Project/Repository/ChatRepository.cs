using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.DataAccess;
using DataAccess;

namespace Repository
{
    public class ChatRepository : IChatRepository
    {
        public Task CreateChat(Chat chat)
        {
            return ChatDAO.CreateChat(chat);
        }
    }
}
