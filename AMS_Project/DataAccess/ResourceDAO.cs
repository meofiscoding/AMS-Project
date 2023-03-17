using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.DataAccess;

namespace DataAccess
{
    public class ResourceDAO
    {
        public static Task CreateResources(List<Resource> resources)
        {
            using (var db = new AMSContext())
            {
                db.Resources.AddRangeAsync(resources);
                return db.SaveChangesAsync();
            }
        }
    }
}
