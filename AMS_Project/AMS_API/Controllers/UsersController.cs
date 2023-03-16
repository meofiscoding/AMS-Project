using BusinessObject.DataAccess;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AMS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        public UsersController(IUserRepository userRepository, IRoleRepository roleRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }


        // GET: api/<UsersController>
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult Get(string? search)
        {
            var userList = new List<User>();
            if (search == null)
            {
                userList = _userRepository.GetUsers();
            }
            else
            {
               userList =_userRepository.FindUsers(search);
            }

            //assign role for each student in classStudent in UserRole field
            foreach (var student in userList)
            {
                var userRole = _roleRepository.GetRole(student.UserRoleId.Value);
                student.UserRole = userRole;
            }

            return Ok(userList);
        }
        
    }
}
