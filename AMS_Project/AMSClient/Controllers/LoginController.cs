using BusinessObject.DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace AMSClient.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            //get all roles from AMSContext and store in viewbag
            ViewBag.Roles = new AMSContext().Roles.ToList();
            return View();
        }
    }
}
