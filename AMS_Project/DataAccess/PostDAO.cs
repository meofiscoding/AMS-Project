using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.DataAccess;

namespace DataAccess
{
    public class PostDAO
    {
        public static Task CreatePost(Post post)
        {
            using (var db = new AMSContext())
            {
                db.Posts.Add(post);
                return db.SaveChangesAsync();
            }
        }
    }
}
