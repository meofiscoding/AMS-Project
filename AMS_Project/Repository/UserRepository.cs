﻿using System;
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
    }
}