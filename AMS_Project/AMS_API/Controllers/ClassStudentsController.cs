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
using System.Security.Claims;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace AMS_API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ClassStudentsController : ControllerBase
    {
        private readonly IClassStudentRepository _classStudentRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IClassRepository _classRepository;

        public ClassStudentsController(IClassStudentRepository classStudentRepository, IUserRepository userRepository, IRoleRepository roleRepository, IClassRepository classRepository)
        {
            _classStudentRepository = classStudentRepository;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _classRepository = classRepository;
        }

        // GET: api/ClassStudents
        //get all class that belong to that user id
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult GetClassStudents()
        {
            var userId = GetUserId();

            if (userId == 0)
            {
                return BadRequest("Invalid user email");
            }

            var user = _userRepository.GetUserById(userId);
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

        [HttpPost("JoinClass/{classCode}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> JoinClass(string classCode)
        {
            var userId = GetUserId();

            if (userId == 0)
            {
                return BadRequest("Invalid user email");
            }

            var user = _userRepository.GetUserById(userId);
            if (user == null)
            {
                return BadRequest("User not found");
            }

            //check if the class code is exist in Class table
            var classExist = _classRepository.GetClassByCode(classCode);
            if (classExist == null)
            {
                return BadRequest("Class code is not exist");
            }else{
                //check if user is already registered
                bool isEnrolled = _classStudentRepository.CheckIfUserIsEnrolled(classExist.Id, userId);
                if (isEnrolled)
                {
                    return BadRequest("You are already enrolled in this class");
                }
                //add student to classStudent
                var classStudent = new ClassStudent
                {
                    IdClass = classExist.Id,
                    IdStudent = userId
                };
                await _classStudentRepository.CreateClassStudent(classStudent);
                return Ok(classExist);
            }
        }

        //get user id from token
        private int GetUserId()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var userIdClaim = identity?.Claims.FirstOrDefault(c => c.Type == "ID");
            if(userIdClaim == null)
            {
                return 0;
            }
            else
            {
                return int.Parse(userIdClaim.Value);
            }
        }

        // GET: api/ClassStudents/5
        [HttpGet("{classId}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<ClassStudent>> GetClassStudent(int classId)
        {
            List<User> classStudent = await _classStudentRepository.GetClassStudent(classId);

            //get user id from token
            var userId = GetUserId();

             // Check if the user is enrolled in the class
            if (!classStudent.Any(x => x.Id == userId))
            {
                return Forbid("You are not enrolled in this class");
            }

            //assign role for each student in classStudent in UserRole field
            foreach (var student in classStudent)
            {
                var userRole = _roleRepository.GetRole(student.UserRoleId.Value);
                student.UserRole = userRole;
            }

            return Ok(classStudent);
        }

        // POST: api/ClassStudents/{classId}/students
        [HttpPost]
        [Route("{classId}/students")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> AddStudentsToClass(int classId, [FromBody]List<int> userIds)
        {
            //retrieves the students from the database based on the list of student IDs
            foreach (var item in userIds)
            {
                var student = _userRepository.GetUserById(item);
                if (student == null)
                {
                    return BadRequest("Student not found");
                }
                //check if the student is already enrolled in the class
                if (_classStudentRepository.CheckIfStudentIsEnrolled(classId, item))
                {
                    return BadRequest("Student is already enrolled in this class");
                }

                //add student to class
                await _classStudentRepository.AddStudentToClass(classId, item);
            }
                return Ok("Student added to class");
        }
    }
}
