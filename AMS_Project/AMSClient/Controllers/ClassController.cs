using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessObject.DataAccess;

namespace AMSClient.Controllers
{
    public class ClassController : Controller
    {
      
        // GET: ClassStudents
        public async Task<IActionResult> Details()
        {
            return View();
        }

       
    }
}
