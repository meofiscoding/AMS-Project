using System;
using System.Collections.Generic;

namespace BusinessObject.DataAccess
{
    public partial class Assignment
    {
        public Assignment()
        {
            Submissions = new HashSet<Submission>();
            Resources = new HashSet<Resource>();
        }

        public int Id { get; set; }
        public string AssignmentName { get; set; } = null!;
        public string AssignmentDescription { get; set; } = null!;
        public DateTime AssignmentDeadline { get; set; }
        public int? TeacherId { get; set; }
        public int? ClassId { get; set; }
        public int? GroupId { get; set; }

        public virtual Class? Class { get; set; }
        public virtual Group? Group { get; set; }
        public virtual User? Teacher { get; set; }
        public virtual ICollection<Submission> Submissions { get; set; }

        public virtual ICollection<Resource> Resources { get; set; }
    }
}
