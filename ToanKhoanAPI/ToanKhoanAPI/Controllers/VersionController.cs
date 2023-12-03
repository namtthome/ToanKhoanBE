using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vn.com.pnsuite.toankhoan.dataaccess.interfaces;
using vn.com.pnsuite.toankhoan.Helpers;

namespace vn.com.pnsuite.toankhoan.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class VersionController : BaseApiController
    {
        private readonly IVersionService _versionService;
        public VersionController(IVersionService versionService, IUserService userService) : base(userService)
        {
            _versionService = versionService;
        }
        [HttpGet("get-list")]
        public IActionResult getAllType()
        {
            var response = _versionService.getAll();
            return Ok(response);
        }
    }
}
