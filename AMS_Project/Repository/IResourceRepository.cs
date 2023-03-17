using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.DataAccess;

namespace Repository
{
    public interface IResourceRepository
    {
        Task CreateResources(List<Resource> resources);
    }
}
