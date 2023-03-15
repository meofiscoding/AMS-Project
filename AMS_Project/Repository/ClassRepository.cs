using BusinessObject.DataAccess;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class ClassRepository : IClassRepository
    {
        public async Task CreateClass(Class @class)
        {
            await ClassDAO.SaveClass(@class);
        }

        public async Task<Class> GetClassById(int id)
        {
            return await ClassDAO.GetClassById(id);
        }
    }
}
