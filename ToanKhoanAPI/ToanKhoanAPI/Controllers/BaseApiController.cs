using Microsoft.AspNetCore.Mvc;
using System;
using vn.com.pnsuite.toankhoan.dataaccess.interfaces;
using vn.com.pnsuite.toankhoan.models.user;

namespace vn.com.pnsuite.toankhoan.Controllers
{
    [ApiController]
    public class BaseApiController : Controller
    {
        protected readonly IUserService _userService;
        public BaseApiController(IUserService userService)
        {
            this._userService = userService;
        }
        public UserModel CurrentUser
        {
            get
            {
                var user = (UserModel)HttpContext.Items["User"];

                //try request authen
                if (user == null)
                {
                    var token = HttpContext.Request.Headers["Token"];
                    var response = this._userService.GetByToken(token);
                    user = (UserModel)response.ActionData;
                }
                return user;
            }
        }
        public String DoNotCheckPermission
        {
            get
            {
                return HttpContext.Request.Headers.ContainsKey("CHECK_PERM") ? HttpContext.Request.Headers["CHECK_PERM"][0] : null;
            }
        }
    }
}
