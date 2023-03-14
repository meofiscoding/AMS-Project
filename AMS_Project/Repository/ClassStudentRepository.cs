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
        public List<ClassStudent> GetClassByUserId(int userId)
        {
            return ClassStudentDAO.GetClassByUserId(userId);
        }
    }
}
