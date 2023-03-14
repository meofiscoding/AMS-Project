﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.DataAccess;

namespace Repository
{
    public interface IClassStudentRepository
    {
        //get all class by user id
        List<ClassStudent> GetClassByUserId(int userId);
    }
}
