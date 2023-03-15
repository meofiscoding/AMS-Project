using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.DataAccess;
using DataAccess;

namespace Repository
{
    public class ClassStudentRepository : IClassStudentRepository
    {
        //create classStudent
        public Task CreateClassStudent(ClassStudent classStudent)
        {
            return ClassStudentDAO.CreateClassStudent(classStudent);
        }

        public List<Class> GetClassByUserId(int userId)
        {
            return ClassStudentDAO.GetClassByUserId(userId);
        }

        public Task<List<User>> GetClassStudent(int classId)
        {
            return ClassStudentDAO.GetClassStudent(classId);
        }
    }
}
