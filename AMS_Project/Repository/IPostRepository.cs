﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.DataAccess;

namespace Repository
{
    public interface IPostRepository
    {
        Task CreatePost(Post post);
        Task<List<Post>> GetPostsByClassId(int classId);
    }
}
