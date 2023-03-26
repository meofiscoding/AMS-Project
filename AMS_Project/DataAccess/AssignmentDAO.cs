 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.DataAccess;

namespace DataAccess
{
    public class AssignmentDAO
    {
        public static async Task AddAssignment(Assignment assignment)
        {
            using (var db = new AMSContext())
            {
                db.Assignments.Add(assignment);
                await db.SaveChangesAsync();
                // Eagerly load Resources property
                await db.Entry(assignment).Collection(a => a.Resources).LoadAsync();
            }
        }
    }
}
