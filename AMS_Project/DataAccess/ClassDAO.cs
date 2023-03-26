using BusinessObject.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class ClassDAO
    {
        //save class
        public static async Task SaveClass(Class @class)
        {
            using (var context = new AMSContext())
            {
                context.Classes.Add(@class);
                await context.SaveChangesAsync();
            }
        }

        //get class by id
        public static async Task<Class> GetClassById(int id)
        {
            using (var context = new AMSContext())
            {
                return await context.Classes.FindAsync(id);
            }
        }

        public static Class GetClassByCode(string classCode)
        {
            using (var context = new AMSContext())
            {
                return context.Classes.Where(x => x.ClassCode == classCode).FirstOrDefault();
            }
        }
    }
}
