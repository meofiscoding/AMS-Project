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

namespace AMS_API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ClassesController : ControllerBase
    {
        private readonly IClassRepository _classRepository;


        public ClassesController(IClassRepository classRepository)
        {
            _classRepository = classRepository;
        }

         // POST: api/Classes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
         public async Task<ActionResult<Class>> PostClass(Class @class)
        {
            await _classRepository.CreateClass(@class);
            return CreatedAtAction("GetClass", new { id = @class.Id }, @class);
        }

        //// GET: api/Classes/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Class>> GetClass(int id)
        //{
        //    var @class = await _context.Classes.FindAsync(id);

        //    if (@class == null)
        //    {
        //        return NotFound();
        //    }

        //    return @class;
        //}

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
