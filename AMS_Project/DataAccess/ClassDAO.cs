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
        public static Task SaveClass(Class @class)
        {
            using (var context = new AMSContext())
            {
                context.Classes.Add(@class);
                return context.SaveChangesAsync();
            }
        }
    }
}
