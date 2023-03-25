using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.DataAccess;
using DataAccess;

namespace Repository
{
    public class GroupStudentRepository : IGroupStudentRepository
    {
        public Task CreateGroupStudent(int studentId, int v)
        {
            //call GroupStuentDAO
            return GroupStudentDAO.CreateGroupStudent(studentId, v);
        }

        public List<GroupStudent> GetGroupStudentsByGroupId(int id)
        {
            //call GroupStudentDAO
            return GroupStudentDAO.GetGroupStudentsByGroupId(id);
        }
    }
}
