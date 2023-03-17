using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.DataAccess;
using DataAccess;

namespace Repository
{
    public class CommentRepository : ICommentRepository
    {
        public ICollection<Comment> GetCommentsByPostId(int id)
        {
            return CommentDAO.GetCommentsByPostId(id);
        }
    }
}
