﻿using BussinessObject.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IRoleRepository
    {
        //get role by name
        Role GetRoleByName(string roleName);
    }
}
