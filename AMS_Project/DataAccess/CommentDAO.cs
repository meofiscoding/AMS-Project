using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.DataAccess;

namespace DataAccess
{
    public class CommentDAO
    {
        public static async Task CreateComment(Comment comment)
        {
            using (var db = new AMSContext())
            {
                db.Comments.Add(comment);
                await db.SaveChangesAsync();
            }
        }

        public static ICollection<Comment> GetCommentsByPostId(int id)
        {
            using (var db = new AMSContext())
            {
                var comments = db.Comments.Where(c => c.PostId == id).ToList();
                // Eagerly load Resources property
                foreach (var comment in comments)
                {
                    comment.User = db.Users.FirstOrDefault(u => u.Id == comment.UserId);
                    //assign role to user
                    comment.User.UserRole = db.Roles.FirstOrDefault(r => r.Id == comment.User.UserRoleId);
                    //make sure comment.Resource is not null
                    if (comment.Resource != null){
                        //load resource collection for comments
                        db.Entry(comment).Reference(c => c.Resource).Load();
                    }
                }
                return comments;
            }
        }
    }
}
