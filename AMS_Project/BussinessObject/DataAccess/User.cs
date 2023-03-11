using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace BussinessObject.DataAccess
{
    public partial class User : IdentityUser<int>
    {
        public User()
        {
            Assignments = new HashSet<Assignment>();
            ChatReceivers = new HashSet<Chat>();
            ChatSenders = new HashSet<Chat>();
            ClassStudents = new HashSet<ClassStudent>();
            Classes = new HashSet<Class>();
            GroupStudents = new HashSet<GroupStudent>();
            Groups = new HashSet<Group>();
            Submissions = new HashSet<Submission>();
        }

        public override int Id { get; set; }
        public override string UserName { get; set; } = null!;
        public string UserPassword { get; set; } = null!;
        public string UserEmail { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public int? UserRoleId { get; set; }

        public virtual Role? UserRole { get; set; }
        public virtual ICollection<Assignment> Assignments { get; set; }
        public virtual ICollection<Chat> ChatReceivers { get; set; }
        public virtual ICollection<Chat> ChatSenders { get; set; }
        public virtual ICollection<ClassStudent> ClassStudents { get; set; }
        public virtual ICollection<Class> Classes { get; set; }
        public virtual ICollection<GroupStudent> GroupStudents { get; set; }
        public virtual ICollection<Group> Groups { get; set; }
        public virtual ICollection<Submission> Submissions { get; set; }
    }
}
