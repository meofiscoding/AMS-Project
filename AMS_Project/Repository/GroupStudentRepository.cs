using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}
