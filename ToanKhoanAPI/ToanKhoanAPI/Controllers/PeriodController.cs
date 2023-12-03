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
    [Route("api/period")]
    public class PeriodController : BaseApiController
    {
        private readonly IPeriodService _periodService;
        public PeriodController(IPeriodService periodService, IUserService userService) : base(userService)
        {
            _periodService = periodService;
        }
        [HttpPost("get-list")]
        public async Task<IActionResult> GetDebtList([FromBody] CommonRequest request)
        {
            var response = await _periodService.GetPeriodList(this.CurrentUser.CompanyId, this.CurrentUser.Id, request.Values == null ? "[]" : JsonSerializer.Serialize(request.Values));
            return Ok(response);
        }
        [HttpPost("change-state")]
        public async Task<IActionResult> ChangeState([FromBody] CommonRequest request)
        {
            var response = await _periodService.UpdatePeriodState(this.CurrentUser.CompanyId, this.CurrentUser.Id, request.Values == null ? "[]" : JsonSerializer.Serialize(request.Values));
            return Ok(response);
        }
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] dynamic request)
        {
            var response = await _periodService.CreatePeriod(this.CurrentUser.CompanyId, this.CurrentUser.Id, JsonSerializer.Serialize(request));
            return Ok(response);
        }
    }
}
