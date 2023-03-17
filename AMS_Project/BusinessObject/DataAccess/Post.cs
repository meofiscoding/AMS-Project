using System;
using System.Collections.Generic;

namespace BusinessObject.DataAccess
{
    public partial class Post
    {
        public Post()
        {
            Comments = new HashSet<Comment>();
            Resources = new HashSet<Resource>();
        }

        public int Id { get; set; }
        public string PostContent { get; set; } = null!;
        public int ClassId { get; set; }
        public int UserId { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual Class Class { get; set; } = null!;
        public virtual User User { get; set; } = null!;
        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<Resource> Resources { get; set; }
    }
}
