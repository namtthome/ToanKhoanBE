using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vn.com.pnsuite.common.models;
using vn.com.pnsuite.toankhoan.dataaccess.interfaces;
using vn.com.pnsuite.toankhoan.Helpers;
using vn.com.pnsuite.toankhoan.models.authentication;

namespace vn.com.pnsuite.toankhoan.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : BaseApiController
    {
        //private readonly IUserService _userService;

        public AuthenticationController(IUserService userService) : base(userService)
        {
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] AuthenticateRequest model)
        {
            var response = _userService.Authenticate(model, Clients.GetIp(HttpContext));

            if (response.ActionResult == ActionResultData.Success)
            {
                SetTokenCookie(response.ActionData.RefreshToken);
            }

            return Ok(response);
        }

        [HttpPost("refresh-token")]
        public IActionResult RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            //var response = _userService.RefreshToken(refreshToken, ipAddress());
            var response = _userService.RefreshToken(refreshToken, Clients.GetIp(HttpContext));

            if (response == null)
                return Unauthorized(new { message = "Invalid token" });

            SetTokenCookie(response.ActionData.RefreshToken);

            return Ok(response);
        }
        [HttpPost("refresh-token-local")]
        public IActionResult RefreshToken([FromBody] RefreshTokenRequest request)
        {
            //var refreshToken = Request.Cookies["refreshToken"];
            //var response = _userService.RefreshToken(refreshToken, ipAddress());
            var response = _userService.RefreshToken(request.RefreshToken, Clients.GetIp(HttpContext));

            if (response == null)
                return Unauthorized(new { message = "Invalid token" });

            SetTokenCookie(response.ActionData.RefreshToken);

            return Ok(response);
        }
        [Authorize]
        [HttpPost("Logout")]
        public IActionResult Logout([FromBody] RevokeTokenRequest model)
        {
            // accept token from request body or cookie
            var token = model.Token ?? Request.Cookies["refreshToken"];

            if (string.IsNullOrEmpty(token))
                return BadRequest(new { message = "Token is required" });

            //var response = _userService.RevokeToken(token, ipAddress());
            var response = _userService.RevokeToken(token, Clients.GetIp(HttpContext));

            if (!response)
                return NotFound(new { message = "Token not found" });

            return Ok(new { message = "Token revoked" });
        }
        private void SetTokenCookie(string token)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(7)
            };
            Response.Cookies.Append("refreshToken", token, cookieOptions);
        }
    }
}
