﻿using BusinessObject.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IClassRepository
    {
        Task CreateClass(Class @class);
        Class GetClassByCode(string classCode);
        Task<Class> GetClassById(int id);
    }
}
