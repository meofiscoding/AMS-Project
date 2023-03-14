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
        public static List<Class> GetClassByUserId(int userId)
        {
            using (var context = new AMSContext())
            {
                //get all class by user id
                var classStudents = context.ClassStudents.Where(cs => cs.IdStudent == userId).ToList();
                //get classid of each classStudent and reuturn list of class that match classid
                return classStudents.Select(cs => context.Classes.FirstOrDefault(c => c.Id == cs.IdClass)).ToList();
            }
        }

        //create classStudent
        public static async Task CreateClassStudent(ClassStudent classStudent)
        {
            using (var context = new AMSContext())
            {
                context.ClassStudents.Add(classStudent);
                await context.SaveChangesAsync();
            }
        }
    }
}
