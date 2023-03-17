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
        public static ICollection<Comment> GetCommentsByPostId(int id)
        {
            using (var db = new AMSContext())
            {
                var comments = db.Comments.Where(c => c.PostId == id).ToList();
                // Eagerly load Resources property
                foreach (var comment in comments)
                {
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
