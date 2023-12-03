using Microsoft.AspNetCore.Mvc;
using vn.com.pnsuite.hrm.dataaccess.interfaces;
using vn.com.pnsuite.toankhoan.Controllers;
using vn.com.pnsuite.toankhoan.dataaccess.interfaces;

namespace vn.com.pnsuite.hrm.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CompanyController : BaseApiController
    {
        private readonly ICompanyService _companyService;
        public CompanyController(ICompanyService companyService, IUserService userService) : base(userService)
        {
            _companyService = companyService;
        }
    }
}
