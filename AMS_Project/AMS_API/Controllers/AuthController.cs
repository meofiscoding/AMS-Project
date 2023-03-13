using AMS_API.ViewModel;
using BusinessObject.DataAccess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Repository;
using Swashbuckle.Swagger;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AMS_API.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IUserRepository _userRepository;
        private IRoleRepository _roleRepository;
        private readonly IConfiguration _configuration;
        private readonly AMSContext _context;

        public AuthController(IConfiguration configuration, AMSContext context, IUserRepository userRepository, IRoleRepository roleRepository)
        {
            _configuration = configuration;
            _context = context;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }

        // POST api/<AuthController>/register
        [HttpPost("register")]
        public IActionResult Register(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data.");
            }
            var userExists = _userRepository.GetUserByEmail(user.UserEmail);
            if (userExists != null)
            {
                return BadRequest("User already exists.");
            }

            var userRole = _roleRepository.GetRole(user.UserRoleId.Value);

            //hashing password
            var passwordHasher = new PasswordHasher<User>();
            user.UserPassword = passwordHasher.HashPassword(user, user.UserPassword);

            var model = new User
            {
                UserEmail = user.UserEmail,
                UserPassword = user.UserPassword,
                UserRoleId = userRole.Id,
                //UserRole = userRole,
                FullName = user.FullName
            };

            _userRepository.AddUser(model);
            return Ok("User registered successfully.");
        }

        [HttpPost("login")]
        public IActionResult Login(User userModel)
        {
            //find user
            var user = _userRepository.GetUserByEmail(userModel.UserEmail);
            if (user == null)
            {
                return Unauthorized();
            }

            //verify password
            var passwordHasher = new PasswordHasher<User>();
            var result = passwordHasher.VerifyHashedPassword(user, user.UserPassword, userModel.UserPassword);

            //check if password is correct
            if (result != PasswordVerificationResult.Success)
            {
                return BadRequest("Invalid password.");
            }

            //get role
            var role = _roleRepository.GetRole(user.UserRoleId.Value);

            //generate token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserEmail),
                    new Claim(ClaimTypes.Role, role.RoleName)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new { token = tokenString });
        }

    }
}
