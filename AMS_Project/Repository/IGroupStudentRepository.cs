using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.DataAccess;

namespace Repository
{
    public interface IGroupStudentRepository
    {
        Task CreateGroupStudent(int studentId, int v);
        List<GroupStudent> GetGroupStudentsByGroupId(int id);
    }
}
