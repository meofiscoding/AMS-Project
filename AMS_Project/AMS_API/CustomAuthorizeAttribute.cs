using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using BusinessObject.DataAccess;
using System.Security.Claims;
using Repository;

namespace AMS_API
{
    //[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class CustomAuthorizeAttribute
    {
        //private readonly string _role;
        //private readonly Type _userRepository;

        //public CustomAuthorizeAttribute(string role, Type userRepository)
        //{
        //    _role = role;
        //    _userRepository = userRepository;
        //}

        //public void OnAuthorization(AuthorizationFilterContext context)
        //{
        //    var userRepository = context.HttpContext.RequestServices.GetService(_userRepository) as IUserRepository;

        //    var identity = context.HttpContext.User.Identity as ClaimsIdentity;
        //    var userIdClaim = identity?.Claims.FirstOrDefault(c => c.Type == "ID");

        //    var user = userRepository.GetUserById(int.Parse(userIdClaim.Value));

        //    if (user == null || !user.UserRole.RoleName.ToLower().Equals(_role.ToLower()))
        //    {
        //        context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
        //    }
        //}
    }


}
