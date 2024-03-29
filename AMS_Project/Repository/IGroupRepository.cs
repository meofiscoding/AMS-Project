﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.DataAccess;

namespace Repository
{
    public interface IGroupRepository
    {
        bool CheckGroupExists(Dictionary<string, List<string>>.KeyCollection keys, int classId);
        int CreateGroup(string group, int classId);
        Group GetGroupById(int groupId);
        List<Group> GetGroupsByClassId(int classId);
        Task UpdateGroup(Group group);
    }
}
