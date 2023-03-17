using System;
using System.Collections.Generic;

namespace BusinessObject.DataAccess
{
    public partial class Resource
    {
        public Resource()
        {
            Comments = new HashSet<Comment>();
            Assignments = new HashSet<Assignment>();
            ClassUsers = new HashSet<ClassStudent>();
            Groups = new HashSet<Group>();
            Posts = new HashSet<Post>();
            Submissions = new HashSet<Submission>();
        }

        public int Id { get; set; }
        public string ResourceName { get; set; } = null!;
        public string Type { get; set; } = null!;
        public string FileUrl { get; set; } = null!;

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<Assignment> Assignments { get; set; }
        public virtual ICollection<ClassStudent> ClassUsers { get; set; }
        public virtual ICollection<Group> Groups { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<Submission> Submissions { get; set; }
    }
}
