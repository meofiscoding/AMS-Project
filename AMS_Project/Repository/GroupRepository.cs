using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.DataAccess;
using DataAccess;

namespace Repository
{
    public class GroupRepository : IGroupRepository
    {
        public bool CheckGroupExists(Dictionary<string, List<string>>.KeyCollection keys, int classId)
        {
            return GroupDAO.CheckGroupExists(keys, classId);
        }

        public int CreateGroup(string group, int classId)
        {
            return GroupDAO.CreateGroup(group, classId);
        }

        public List<Group> GetGroupsByClassId(int classId)
        {
            return GroupDAO.GetGroupsByClassId(classId);
        }
    }
}
