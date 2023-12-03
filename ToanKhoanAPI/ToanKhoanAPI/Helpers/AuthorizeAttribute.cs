using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using vn.com.pnsuite.toankhoan.dataaccess.interfaces;
using vn.com.pnsuite.toankhoan.models.user;

namespace vn.com.pnsuite.toankhoan.Helpers
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            //var refreshToken = context.HttpContext.Request.Cookies["refreshToken"].ToString();
            var user = (UserModel)context.HttpContext.Items["User"];
            if (user == null)
            {
                //Context Timeout
                //try request authen
                IUserService _userService = (IUserService)context.HttpContext.RequestServices.GetService(typeof(IUserService));

                var token = context.HttpContext.Request.Headers["Token"];
                var response = _userService.GetByToken(token);
                user = (UserModel)response.ActionData;
                context.HttpContext.Items["User"] = user;

                if (user == null)
                {
                    context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
                }

            }
        }
    }
}
