using System;
using System.Collections.Generic;

namespace BussinessObject.DataAccess
{
    public partial class Submission
    {
        public Submission()
        {
            Resources = new HashSet<Resource>();
        }

        public int Id { get; set; }
        public string Content { get; set; } = null!;
        public double? Grade { get; set; }
        public string? TeacherFeedback { get; set; }
        public int? ClassUserId { get; set; }
        public DateTime? SubmissionDate { get; set; }
        public int? AssignmentId { get; set; }
        public int? StudentId { get; set; }

        public virtual Assignment? Assignment { get; set; }
        public virtual ClassStudent? ClassUser { get; set; }
        public virtual User? Student { get; set; }

        public virtual ICollection<Resource> Resources { get; set; }
    }
}
