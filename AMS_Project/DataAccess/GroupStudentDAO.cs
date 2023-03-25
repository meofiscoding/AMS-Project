using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.DataAccess;

namespace DataAccess
{
    public class GroupStudentDAO
    {
        public static Task CreateGroupStudent(int studentId, int groupId)
        {
            //add to database
            using (var db = new AMSContext())
            {
                var groupStudent = new GroupStudent
                {
                    UserId = studentId,
                    GroupId = groupId
                };
                db.GroupStudents.Add(groupStudent);
                db.SaveChanges();
            }
            return Task.CompletedTask;
        }
    }
}
