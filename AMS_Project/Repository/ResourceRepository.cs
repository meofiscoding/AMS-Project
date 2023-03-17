using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.DataAccess;
using DataAccess;

namespace Repository
{
    public class ResourceRepository : IResourceRepository
    {
        public Task CreateResources(List<Resource> resources)
        {
            return ResourceDAO.CreateResources(resources);
        }

        public ICollection<Resource> GetResourcesByPostId(int id)
        {
            return ResourceDAO.GetResourcesByPostId(id);
        }
    }
}
