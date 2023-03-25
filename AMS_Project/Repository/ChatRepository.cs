using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;

namespace Repository
{
    public class ChatRepository : IChatRepository
    {
        public void SaveMessage(string groupName, string user, string message)
        {
            // ChatDAO.SaveMessage(groupName, user, message);
        }
    }
}
