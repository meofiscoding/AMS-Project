using BusinessObject.DataAccess;

namespace DataAccess
{
    public class UserDAO
    {
        //add User to database
        public static void AddUser(User u)
        {
            try
            {
                using (var db = new AMSContext())
                {
                    db.Users.Add(u);
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}