using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BussinessObject.DataAccess;

namespace Repository
{
    public interface IUserRepository
    {
        //Add user
        void AddUser(User user);
    }
}
