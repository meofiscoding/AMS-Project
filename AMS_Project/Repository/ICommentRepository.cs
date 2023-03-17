using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.DataAccess;

namespace Repository
{
    public interface ICommentRepository
    {
        Task CreateComment(Comment comment);
        ICollection<Comment> GetCommentsByPostId(int id);
    }
}
