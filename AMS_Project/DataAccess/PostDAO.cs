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
                var posts = db.Posts.Where(p => p.ClassId == classId).OrderByDescending(p => p.CreatedAt).ToList();
                //get post with comments belong to it
                foreach (var post in posts)
                {
                    post.Comments = db.Comments.Where(c => c.PostId == post.Id).ToList();
                    //get user of each comment
                    foreach (var comment in post.Comments)
                    {
                        comment.User = db.Users.FirstOrDefault(u => u.Id == comment.UserId);
                        //assign role to user
                        comment.User.UserRole = db.Roles.FirstOrDefault(r => r.Id == comment.User.UserRoleId);
                        //make sure comment.Resource is not null
                        if (comment.Resource != null)
                        {
                            //load resource collection for comments
                            db.Entry(comment).Reference(c => c.Resource).Load();
                        }
                    }
                }
                // Eagerly load Resources and comment property
                foreach (var post in posts)
                {
                    db.Entry(post).Collection(p => p.Resources).Load();
                    db.Entry(post).Collection(p => p.Comments).Load();
                }
                return Task.FromResult(posts);
            }
        }
    }
}
