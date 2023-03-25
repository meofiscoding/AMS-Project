using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.DataAccess;

namespace DataAccess
{
    public class GroupDAO
    {
        public static bool CheckGroupExists(Dictionary<string, List<string>>.KeyCollection keys, int classId)
        {
            using (var db = new AMSContext())
            {
                foreach (var group in keys)
                {
                    var groupExists = db.Groups.Where(x => x.GroupName == group && x.ClassId == classId).FirstOrDefault();
                    if (groupExists != null)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        public static int CreateGroup(string group, int classId)
        {
            using (var db = new AMSContext())
            {
                var newGroup = new Group
                {
                    GroupName = group,
                    ClassId = classId
                };
                db.Groups.Add(newGroup);
                db.SaveChanges();
                return newGroup.Id;
            }
        }
    }
}
