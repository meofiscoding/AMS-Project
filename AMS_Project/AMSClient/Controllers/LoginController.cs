using Microsoft.AspNetCore.Mvc;

namespace AMSClient.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
