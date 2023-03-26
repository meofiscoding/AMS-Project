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
        public Task AddStudentToClass(int classId, int item)
        {
            return ClassStudentDAO.CreateClassStudent(new ClassStudent
            {
                IdClass = classId,
                IdStudent = item
            });
        }

        public bool CheckIfStudentIsEnrolled(int classId, int item)
        {
            return ClassStudentDAO.CheckIfStudentIsEnrolled(classId, item);
        }

        public bool CheckIfUserIsEnrolled(int id, int userId)
        {
            return ClassStudentDAO.CheckIfUserIsEnrolled(id, userId);
        }

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
