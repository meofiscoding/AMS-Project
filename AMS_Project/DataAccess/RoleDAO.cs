using BussinessObject.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public static class RoleDAO
    {
        //get role by name
        public static Role GetRoleByName(string roleName)
        {
            using (var db = new AMSContext())
            {
                return db.Roles.Where(r => r.RoleName.ToLower().Equals(roleName.ToLower())).FirstOrDefault();
            }
        }
    }
}
