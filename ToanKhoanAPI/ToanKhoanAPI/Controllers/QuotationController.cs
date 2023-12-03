using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Text.Json;
using vn.com.pnsuite.common.models;
using vn.com.pnsuite.toankhoan.Controllers;
using vn.com.pnsuite.toankhoan.dataaccess.interfaces;
using vn.com.pnsuite.toankhoan.dataaccess.Interfaces;
using vn.com.pnsuite.toankhoan.Helpers;

namespace Pnsuite.ToanKhoan.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class QuotationController : BaseApiController
    {
        private readonly IQuotationService _quotationService;
        public QuotationController(IUserService userService, IQuotationService quotationService) : base(userService)
        {
            _quotationService = quotationService;
        }
        [HttpPost("get-all")]
        public async Task<IActionResult> GetAllQuotationBySearchAsync(CommonRequest request)
        {
            var response = await _quotationService.GetAllQuotationBySearchAsync(CurrentUser.Id, CurrentUser.CompanyId, request.Values == null ? "[]" : JsonSerializer.Serialize(request.Values));
            return Ok(response);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateQuotationAsync([FromBody] dynamic request)
        {
            var response = await _quotationService.CreateQuotationAsync(CurrentUser.CompanyId, CurrentUser.Id, JsonSerializer.Serialize(request));
            return Ok(response);
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdateQuotationAsync([FromBody] dynamic request)
        {
            var response = await _quotationService.UpdateQuotationAsync(CurrentUser.CompanyId, CurrentUser.Id, JsonSerializer.Serialize(request));
            return Ok(response);
        }

        [HttpPost("detail")]
        public async Task<IActionResult> GetQuotationDetail(CommonRequest request)
        {
            var response = await _quotationService.GetQuotationDetail(CurrentUser.CompanyId, CurrentUser.Id, request.JsonValue);
            return Ok(response);
        }
        [HttpPost("copy")]
        public async Task<IActionResult> GetCopyQuotation(CommonRequest request)
        {
            var response = await _quotationService.GetCopyQuotation(CurrentUser.CompanyId, CurrentUser.Id, request.JsonValue);
            return Ok(response);
        }

        [HttpPost("delete")]
        public async Task<IActionResult> DeleteQuotationAsync(CommonRequest request)
        {
            var response = await _quotationService.DeleteQuotationAsync(CurrentUser.CompanyId, CurrentUser.Id, request.JsonValue);
            return Ok(response);
        }
    }
}
