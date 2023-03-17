using System;
using System.Collections.Generic;

namespace BusinessObject.DataAccess
{
    public partial class Class
    {
        public Class()
        {
            Assignments = new HashSet<Assignment>();
            ClassStudents = new HashSet<ClassStudent>();
            Groups = new HashSet<Group>();
            Posts = new HashSet<Post>();
        }

        public int Id { get; set; }
        public string ClassName { get; set; } = null!;
        public string? ClassDescription { get; set; }
        public string ClassCode { get; set; } = null!;
        public int ClassStatus { get; set; }
        public int? TeacherId { get; set; }
        public DateTime? CreateAt { get; set; }

        public virtual User? Teacher { get; set; }
        public virtual ICollection<Assignment> Assignments { get; set; }
        public virtual ICollection<ClassStudent> ClassStudents { get; set; }
        public virtual ICollection<Group> Groups { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
    }
}
