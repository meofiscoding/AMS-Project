using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BusinessObject.DataAccess;
using Repository;
using System.IdentityModel.Tokens.Jwt;

namespace AMS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassStudentsController : ControllerBase
    {
        private readonly IClassStudentRepository _classStudentRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;

        public ClassStudentsController(IClassStudentRepository classStudentRepository, IUserRepository userRepository, IRoleRepository roleRepository)
        {
            _classStudentRepository = classStudentRepository;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }

        // GET: api/ClassStudents
        [HttpGet]
        public IActionResult GetClassStudents([FromHeader(Name = "Authorization")] string token)
        {
            var userEmail = GetUserIdFromToken(token);

            if (userEmail == null)
            {
                return BadRequest("Invalid user email");
            }

            var user = _userRepository.GetUserByEmail(userEmail);
            if (user == null)
            {
                return BadRequest("User not found");
            }

            var userRole = _roleRepository.GetRole(user.UserRoleId.Value);

            switch (userRole.RoleName)
            {
                case "Teacher":
                    return Ok(new { Role = userRole.RoleName, Classes = _classStudentRepository.GetClassByUserId(user.Id) });
                case "Student":
                    return Ok(new { Role = userRole.RoleName, Classes = _classStudentRepository.GetClassByUserId(user.Id) });
                default:
                    return BadRequest("Invalid user role");
            }
        }

        private string GetUserIdFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);

            if (jwtToken == null || jwtToken.Payload == null)
            {
                throw new ArgumentException("Invalid JWT token");
            }

            var claims = jwtToken.Payload.Claims;

            if (claims == null)
            {
                throw new ArgumentException("JWT token does not contain any claims");
            }

            var userNameClaim = claims.FirstOrDefault(c => c.Type == "unique_name");

            if (userNameClaim == null || string.IsNullOrEmpty(userNameClaim.Value))
            {
                throw new ArgumentException("JWT token does not contain a valid user email");
            }

            return userNameClaim.Value;
        }

    }
}
