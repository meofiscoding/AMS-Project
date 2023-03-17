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

        public static ICollection<Resource> GetResourcesByPostId(int id)
        {
           //load resources collection of post id
            using (var db = new AMSContext())
            {
                var post = db.Posts.Where(p => p.Id == id).FirstOrDefault();
                db.Entry(post).Collection(p => p.Resources).Load();
                return post.Resources;
            }
        }
    }
}
