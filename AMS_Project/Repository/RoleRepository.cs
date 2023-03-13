﻿using BusinessObject.DataAccess;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class RoleRepository : IRoleRepository
    {
        public Role GetRole(int id)
        {
            return RoleDAO.GetRole(id);
        }
    }
}
