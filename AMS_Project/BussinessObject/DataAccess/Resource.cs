using System;
using System.Collections.Generic;

namespace BussinessObject.DataAccess
{
    public partial class Resource
    {
        public Resource()
        {
            Assignments = new HashSet<Assignment>();
            ClassUsers = new HashSet<ClassStudent>();
            Groups = new HashSet<Group>();
            Submissions = new HashSet<Submission>();
        }

        public int Id { get; set; }
        public string ResourceName { get; set; } = null!;
        public string Type { get; set; } = null!;
        public string FileUrl { get; set; } = null!;

        public virtual ICollection<Assignment> Assignments { get; set; }
        public virtual ICollection<ClassStudent> ClassUsers { get; set; }
        public virtual ICollection<Group> Groups { get; set; }
        public virtual ICollection<Submission> Submissions { get; set; }
    }
}
