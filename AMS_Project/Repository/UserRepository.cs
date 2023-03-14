using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.DataAccess;
using DataAccess;

namespace Repository
{
    public class UserRepository : IUserRepository
    {
        //add user
        public void AddUser(User user)
        {
            //add user to db
            UserDAO.AddUser(user);
        }

        public User GetUserByEmail(string userEmail)
        {
            //get user by email
            return UserDAO.GetUserByEmail(userEmail);
        }

        public User GetUserById(int userId)
        {
            return UserDAO.GetUserById(userId);
        }
    }
}
