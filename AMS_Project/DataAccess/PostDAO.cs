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
        public static async Task CreatePost(Post post)
        {
            using (var db = new AMSContext())
            {
                db.Posts.Add(post);
                await db.SaveChangesAsync();
                // Eagerly load Resources property
                await db.Entry(post).Collection(p => p.Resources).LoadAsync();
            }
        }

        public static Task<List<Post>> GetPostsByClassId(int classId)
        {
            using (var db = new AMSContext())
            {
                var posts = db.Posts.Where(p => p.ClassId == classId).ToList();
                // Eagerly load Resources property
                foreach (var post in posts)
                {
                    db.Entry(post).Collection(p => p.Resources).Load();
                }
                return Task.FromResult(posts);
            }
        }
    }
}
