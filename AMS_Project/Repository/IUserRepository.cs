using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.DataAccess;

namespace Repository
{
    public interface IUserRepository
    {
        //Add user
        void AddUser(User user);
        User GetUserByEmail(string userEmail);
    }
}
