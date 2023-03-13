using System;
using System.Collections.Generic;

namespace BusinessObject.DataAccess
{
    public partial class Group
    {
        public Group()
        {
            Assignments = new HashSet<Assignment>();
            Chats = new HashSet<Chat>();
            GroupStudents = new HashSet<GroupStudent>();
            Resources = new HashSet<Resource>();
        }

        public int Id { get; set; }
        public string GroupName { get; set; } = null!;
        public int? ClassId { get; set; }
        public int? LeaderId { get; set; }

        public virtual Class? Class { get; set; }
        public virtual User? Leader { get; set; }
        public virtual ICollection<Assignment> Assignments { get; set; }
        public virtual ICollection<Chat> Chats { get; set; }
        public virtual ICollection<GroupStudent> GroupStudents { get; set; }

        public virtual ICollection<Resource> Resources { get; set; }
    }
}
