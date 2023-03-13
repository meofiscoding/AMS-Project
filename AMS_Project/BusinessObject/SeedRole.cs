using BusinessObject.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject
{
    public static class SeedRole
    {
        public static void Seed(IServiceProvider serviceProvider)
        {
            using (var db = new AMSContext())
            {
                if (!db.Roles.Any())
                {
                    var roles = new List<Role>
                    {
                        new Role { RoleName = "Student" },
                        new Role { RoleName = "Teacher" },
                        new Role { RoleName = "GroupLeader" },
                    };
                    db.Roles.AddRange(roles);
                    db.SaveChanges();
                }
            }
        }
    }
}
