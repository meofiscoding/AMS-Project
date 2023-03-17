using System;
using System.Collections.Generic;

namespace BusinessObject.DataAccess
{
    public partial class Comment
    {
        public Comment()
        {
            InverseParentComment = new HashSet<Comment>();
        }

        public int Id { get; set; }
        public string Content { get; set; } = null!;
        public int PostId { get; set; }
        public int UserId { get; set; }
        public int? ParentCommentId { get; set; }
        public int? ResourceId { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual Comment? ParentComment { get; set; }
        public virtual Post Post { get; set; } = null!;
        public virtual Resource? Resource { get; set; }
        public virtual User User { get; set; } = null!;
        public virtual ICollection<Comment> InverseParentComment { get; set; }
    }
}
