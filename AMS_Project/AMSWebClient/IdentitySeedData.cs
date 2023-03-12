using Microsoft.AspNetCore.Identity;

namespace AMSWebClient
{
    public class IdentitySeedData
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public IdentitySeedData(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task SeedRolesAsync()
        {
            string[] roles = { "Student", "Teacher" };
            foreach (var role in roles)
            {
                bool exists = await _roleManager.RoleExistsAsync(role);
                if (!exists)
                {
                    await _roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }
    }
}
