using AMS_API.ViewModel;
using BussinessObject.DataAccess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Repository;
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
        public IActionResult Register(RegisterViewModel registerViewModel)
        {
            try
            {
                //create user
                var user = new User
                {
                    UserName = registerViewModel.UserName,
                    UserPassword = registerViewModel.Password,
                    UserEmail = registerViewModel.Email,
                    UserRoleId = _roleRepository.GetRoleByName(registerViewModel.Role).Id,
                };
                //add to db
                _userRepository.AddUser(user);
                return Ok(new { message = "User registered successfully" });
            }
            catch (Exception ex)
            {
                //log the exception message
                Console.WriteLine(ex.Message);
                throw;
            }

        }

        [HttpPost("login")]
        public IActionResult Login(LoginViewModel loginViewModel)
        {
            //cast to user
            var user = _context.Users.FirstOrDefault(u => u.UserEmail == loginViewModel.Email);

            //check if user exists
            if (user == null)
            {
                return Unauthorized();
            }
            else
            {
                //hashing loginViewModel.Password
                var passwordHasher = new PasswordHasher<User>();
                var result = passwordHasher.VerifyHashedPassword(user, user.UserPassword.Replace("&#x2B;", "+"), loginViewModel.Password);

                //check if password is correct
                if (result == PasswordVerificationResult.Success)
                {
                    // Create claims based on the user object
                    var claims = new[]
                    {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName)
            };


                    // Generate symmetric security key based on the configuration value
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

                    // Generate JWT
                    var token = new JwtSecurityToken(
                        issuer: _configuration["Jwt:Issuer"],
                        audience: _configuration["Jwt:Audience"],
                        claims: claims,
                        expires: DateTime.UtcNow.AddDays(7),
                        signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

                    // Write the JWT as a string
                    var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

                    // Return the JWT as the response
                    return Ok(new { token = tokenString });
                }
                else
                {
                    return Unauthorized();
                }
            }


        }
    }
}
