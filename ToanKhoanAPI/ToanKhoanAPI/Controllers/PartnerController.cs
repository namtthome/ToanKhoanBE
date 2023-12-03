using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System;
using vn.com.pnsuite.common.dataaccess.repositories;
using vn.com.pnsuite.common.models;
using vn.com.pnsuite.hrm.dataaccess.interfaces;
using vn.com.pnsuite.toankhoan.Controllers;
using vn.com.pnsuite.toankhoan.dataaccess.interfaces;
using vn.com.pnsuite.toankhoan.dataaccess.Interfaces;
using vn.com.pnsuite.toankhoan.models.function;
using System.Text.Json;
using System.Threading.Tasks;
using vn.com.pnsuite.toankhoan.Helpers;

namespace Pnsuite.ToanKhoan.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class PartnerController : BaseApiController
    {
        private readonly IPartnerService _partnerService;
        public PartnerController(IPartnerService partnerService, IUserService userService) : base(userService)
        {
            _partnerService = partnerService;
        }
        [HttpPost("supplier-list")]
        public async Task<IActionResult> GetSupplierList([FromBody] CommonRequest request)
        {
            var response = await _partnerService.GetPartnerList(this.CurrentUser.CompanyId, this.CurrentUser.Id, "SUP", request.Values == null ? "[]" : JsonSerializer.Serialize(request.Values));
            return Ok(response);
        }

        [HttpPost("customer-list")]
        public async Task<IActionResult> GetCustomerList([FromBody] CommonRequest request)
        {
            var response = await _partnerService.GetPartnerList(this.CurrentUser.CompanyId, this.CurrentUser.Id, "CUS", request.Values == null ? "[]" : JsonSerializer.Serialize(request.Values));
            return Ok(response);
        }
    }
}
