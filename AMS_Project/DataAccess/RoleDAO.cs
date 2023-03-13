using BusinessObject.DataAccess;
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
        public static Role GetRole(int id)
        {
            try
            {
                using (var db = new AMSContext())
                {
                    return db.Roles.FirstOrDefault(r => r.Id == id);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
