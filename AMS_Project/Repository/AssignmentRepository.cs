using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.DataAccess;
using DataAccess;

namespace Repository
{
    public class AssignmentRepository : IAssignmentRepository
    {
        public Task AddAssignment(Assignment assignment)
        {
            return AssignmentDAO.AddAssignment(assignment);
        }
    }
}
