using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.DataAccess;
using Microsoft.EntityFrameworkCore;

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

        public static List<GroupStudent> GetGroupStudentsByGroupId(int id)
        {
            using (var db = new AMSContext())
            {
                //get all groups and assign user in each groupstudent
                return db.GroupStudents.Include(gs => gs.User).Where(gs => gs.GroupId == id).ToList();
            }
        }
    }
}
