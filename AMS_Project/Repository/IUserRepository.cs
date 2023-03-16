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
        User GetUserById(int userId);
        //get all user
        List<User> GetUsers();
        //get all user contain search
        List<User> FindUsers(string search);
    }
}
