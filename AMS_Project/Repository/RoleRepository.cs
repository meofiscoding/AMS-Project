using BusinessObject.DataAccess;
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
        public Role GetRoleByName(string roleName)
        {
            return RoleDAO.GetRoleByName(roleName);
        }
    }
}
