using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.DataAccess;

namespace Repository
{
    public interface IClassStudentRepository
    {
        Task CreateClassStudent(ClassStudent classStudent);

        //get all class by user id
        List<Class> GetClassByUserId(int userId);
        Task<List<User>> GetClassStudent(int classId);
        //add class

    }
}
