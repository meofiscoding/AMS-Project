using BusinessObject.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public static class ClassStudentDAO
    {
        //get all class by user id
        public static List<ClassStudent> GetClassByUserId(int userId)
        {
            using (var context = new AMSContext())
            {
                var classStudents = context.ClassStudents.Where(x => x.IdStudent == userId).ToList();
                return classStudents;
            }
        }
    }
}
