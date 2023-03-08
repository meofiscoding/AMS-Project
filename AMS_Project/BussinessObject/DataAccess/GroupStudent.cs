using System;
using System.Collections.Generic;

namespace BussinessObject.DataAccess
{
    public partial class GroupStudent
    {
        public int Id { get; set; }
        public int? GroupId { get; set; }
        public int? UserId { get; set; }

        public virtual Group? Group { get; set; }
        public virtual User? User { get; set; }
    }
}
