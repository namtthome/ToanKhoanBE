using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Threading.Tasks;
using vn.com.pnsuite.common.models;
using vn.com.pnsuite.toankhoan.Controllers;
using vn.com.pnsuite.toankhoan.dataaccess.interfaces;
using vn.com.pnsuite.toankhoan.dataaccess.Interfaces;
using vn.com.pnsuite.toankhoan.dataaccess.Repositories;
using vn.com.pnsuite.toankhoan.Helpers;

namespace Pnsuite.ToanKhoan.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/adjust")]
    public class AdjustDebtController : BaseApiController
    {
        private readonly IAdjustService _adjustService;
        public AdjustDebtController(IAdjustService adjustService, IUserService userService) : base(userService)
        {
            _adjustService = adjustService;
        }
        [HttpPost("list")]
        public async Task<IActionResult> GetAdjustList([FromBody] CommonRequest request)
        {
            var response = await _adjustService.GetAdjustList(this.CurrentUser.CompanyId, this.CurrentUser.Id, request.Values == null ? "[]" : JsonSerializer.Serialize(request.Values));
            return Ok(response);
        }
        [HttpPost("get-by-id")]
        public async Task<IActionResult> GetAdjustById([FromBody] CommonRequest request)
        {
            var response = await _adjustService.GetDetailAdjust(this.CurrentUser.CompanyId, this.CurrentUser.Id, JsonSerializer.Serialize(request.Values));
            return Ok(response);
        }
        [HttpPost("delete")]
        public async Task<IActionResult> DeleteAdjust([FromBody] CommonRequest request)
        {
            var response = await _adjustService.DeleteAdjust(this.CurrentUser.CompanyId, this.CurrentUser.Id, JsonSerializer.Serialize(request.Values));
            return Ok(response);
        }
        [HttpPost("create")]
        public async Task<IActionResult> CreateAdjust([FromBody] dynamic request)
        {
            var response = await _adjustService.CreateOrUpdateAdjust(this.CurrentUser.CompanyId, this.CurrentUser.Id, JsonSerializer.Serialize(request));
            return Ok(response);
        }
        [HttpPost("update")]
        public async Task<IActionResult> UpdateAdjust([FromBody] dynamic request)
        {
            var response = await _adjustService.CreateOrUpdateAdjust(this.CurrentUser.CompanyId, this.CurrentUser.Id, JsonSerializer.Serialize(request));
            return Ok(response);
        }
    }
}
