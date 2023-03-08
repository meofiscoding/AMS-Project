using System;
using System.Collections.Generic;

namespace BussinessObject.DataAccess
{
    public partial class ClassStudent
    {
        public ClassStudent()
        {
            Submissions = new HashSet<Submission>();
            Resources = new HashSet<Resource>();
        }

        public int Id { get; set; }
        public int? IdClass { get; set; }
        public int? IdStudent { get; set; }

        public virtual Class? IdClassNavigation { get; set; }
        public virtual User? IdStudentNavigation { get; set; }
        public virtual ICollection<Submission> Submissions { get; set; }

        public virtual ICollection<Resource> Resources { get; set; }
    }
}
