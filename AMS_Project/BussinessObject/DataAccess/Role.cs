using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace BussinessObject.DataAccess
{
    public partial class Role : IdentityRole<int>
    {
        public Role()
        {
            Users = new HashSet<User>();
        }

        public override int Id { get; set; }
        public string RoleName { get; set; } = null!;

        public virtual ICollection<User> Users { get; set; }
    }
}
