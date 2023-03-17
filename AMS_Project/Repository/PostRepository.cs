using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.DataAccess;
using DataAccess;

namespace Repository
{
    public class PostRepository : IPostRepository
    {
        public Task CreatePost(Post post)
        {
            return PostDAO.CreatePost(post);
        }
    }
}
