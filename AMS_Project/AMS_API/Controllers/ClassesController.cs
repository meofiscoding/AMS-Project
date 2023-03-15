using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BusinessObject.DataAccess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Repository;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace AMS_API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ClassesController : ControllerBase
    {
        private readonly IClassRepository _classRepository;
        private readonly IClassStudentRepository _classStudentRepository;


        public ClassesController(IClassRepository classRepository, IClassStudentRepository classStudentRepository)
        {
            _classRepository = classRepository;
            _classStudentRepository = classStudentRepository;
        }

         // POST: api/Classes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
         public async Task<ActionResult<Class>> PostClass(Class @class)
        {
            await _classRepository.CreateClass(@class);

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var teacherIdClaim = identity?.Claims.FirstOrDefault(c => c.Type == "ID");

            var classStudent = new ClassStudent
            {
                IdClass = @class.Id,
                IdStudent = int.Parse(teacherIdClaim.Value)
            };

            await _classStudentRepository.CreateClassStudent(classStudent);
            return CreatedAtAction("PostClass", new { id = @class.Id }, @class);
        }

        // GET: api/Classes/5
        [HttpGet("{classId}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<Class>> GetClass(int classId)
        {
            var @class = await _classRepository.GetClassById(classId);

            if (@class == null)
            {
                return BadRequest("Class not found");
            }
            return Ok(@class);
        }

        // PUT: api/Classes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutClass(int id, Class @class)
        //{
        //    if (id != @class.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(@class).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!ClassExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // DELETE: api/Classes/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteClass(int id)
        //{
        //    var @class = await _context.Classes.FindAsync(id);
        //    if (@class == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Classes.Remove(@class);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool ClassExists(int id)
        //{
        //    return _context.Classes.Any(e => e.Id == id);
        //}
    }
}
