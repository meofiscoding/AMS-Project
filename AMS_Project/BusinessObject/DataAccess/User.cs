using System;
using System.Collections.Generic;

namespace BusinessObject.DataAccess
{
    public partial class User
    {
        public User()
        {
            Assignments = new HashSet<Assignment>();
            ChatReceivers = new HashSet<Chat>();
            ChatSenders = new HashSet<Chat>();
            ClassStudents = new HashSet<ClassStudent>();
            Classes = new HashSet<Class>();
            Comments = new HashSet<Comment>();
            GroupStudents = new HashSet<GroupStudent>();
            Groups = new HashSet<Group>();
            Posts = new HashSet<Post>();
            Submissions = new HashSet<Submission>();
        }

        public int Id { get; set; }
        public string UserPassword { get; set; } = null!;
        public string UserEmail { get; set; } = null!;
        public string? FullName { get; set; }
        public int? UserRoleId { get; set; }

        public virtual Role? UserRole { get; set; }
        public virtual ICollection<Assignment> Assignments { get; set; }
        public virtual ICollection<Chat> ChatReceivers { get; set; }
        public virtual ICollection<Chat> ChatSenders { get; set; }
        public virtual ICollection<ClassStudent> ClassStudents { get; set; }
        public virtual ICollection<Class> Classes { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<GroupStudent> GroupStudents { get; set; }
        public virtual ICollection<Group> Groups { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<Submission> Submissions { get; set; }
    }
}
