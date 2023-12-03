using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Threading.Tasks;
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
    public class InvoiceController : BaseApiController
    {
        private readonly IInvoiceService _invoiceService;

        public InvoiceController(IUserService userService, IInvoiceService invoiceService) : base(userService)
        {
            _invoiceService = invoiceService;
        }


        #region Return

        [HttpPost("get-all-return")]
        public async Task<IActionResult> GetAllReturnBySearchAsync(CommonRequest request)
        {
            var response = await _invoiceService.GetAllReturnBySearchAsync(CurrentUser.Id, CurrentUser.CompanyId, request.Values == null ? "[]" : JsonSerializer.Serialize(request.Values));
            return Ok(response);
        }

        [HttpPost("create-return")]
        public async Task<IActionResult> CreateReturnAsync([FromBody] dynamic request)
        {
            var response = await _invoiceService.CreateReturnAsync(CurrentUser.CompanyId, CurrentUser.Id, JsonSerializer.Serialize(request));
            return Ok(response);
        }

        [HttpPost("update-return")]
        public async Task<IActionResult> UpdateReturnAsync([FromBody] dynamic request)
        {
            var response = await _invoiceService.UpdateReturnAsync(CurrentUser.CompanyId, CurrentUser.Id, JsonSerializer.Serialize(request));
            return Ok(response);
        }

        [HttpPost("detail-return")]
        public async Task<IActionResult> GetReturnDetailAsync(CommonRequest request)
        {
            var response = await _invoiceService.GetReturnDetailAsync(CurrentUser.CompanyId, CurrentUser.Id, request.JsonValue);
            return Ok(response);
        }
        [HttpPost("copy-return")]
        public async Task<IActionResult> GetCopyDataReturnDetailAsync(CommonRequest request)
        {
            var response = await _invoiceService.GetCopyDataReturnDetailAsync(CurrentUser.CompanyId, CurrentUser.Id, request.JsonValue);
            return Ok(response);
        }
        [HttpPost("delete-return")]
        public async Task<IActionResult> DeleteReturnAsync(CommonRequest request)
        {
            var response = await _invoiceService.DeleteReturnAsync(CurrentUser.CompanyId, CurrentUser.Id, request.JsonValue);
            return Ok(response);
        }

        #endregion

        #region Invoice

        [HttpPost("get-all-invoice")]
        public async Task<IActionResult> GetAllInvoiceBySearchAsync(CommonRequest request)
        {
            var response = await _invoiceService.GetAllInvoiceBySearchAsync(CurrentUser.Id, CurrentUser.CompanyId, request.Values == null ? "[]" : JsonSerializer.Serialize(request.Values));
            return Ok(response);
        }

        [HttpPost("create-invoice")]
        public async Task<IActionResult> CreateInvoiceAsync([FromBody] dynamic request)
        {
            var response = await _invoiceService.CreateInvoiceAsync(CurrentUser.CompanyId, CurrentUser.Id, JsonSerializer.Serialize(request));
            return Ok(response);
        }

        [HttpPost("update-invoice")]
        public async Task<IActionResult> UpdateInvoiceAsync([FromBody] dynamic request)
        {
            var response = await _invoiceService.UpdateInvoiceAsync(CurrentUser.CompanyId, CurrentUser.Id, JsonSerializer.Serialize(request));
            return Ok(response);
        }

        [HttpPost("detail-invoice")]
        public async Task<IActionResult> GetDetailInvoice(CommonRequest request)
        {
            var response = await _invoiceService.GetDetailInvoice(CurrentUser.CompanyId, CurrentUser.Id, request.JsonValue);
            return Ok(response);
        }

        [HttpPost("copy-invoice")]
        public async Task<IActionResult> GetInvoiceCopy(CommonRequest request)
        {
            var response = await _invoiceService.GetCopyInvoice(CurrentUser.CompanyId, CurrentUser.Id, request.JsonValue);
            return Ok(response);
        }
        [HttpPost("create-invoice-from-quotation")]
        public async Task<IActionResult> GetNewInvoiceFromQuotation(CommonRequest request)
        {
            var response = await _invoiceService.GetNewInvoiceFromQuotation(CurrentUser.CompanyId, CurrentUser.Id, request.JsonValue);
            return Ok(response);
        }

        [HttpPost("delete-invoice")]
        public async Task<IActionResult> DeleteInvoiceAsync(CommonRequest request)
        {
            var response = await _invoiceService.DeleteInvoiceAsync(CurrentUser.CompanyId, CurrentUser.Id, request.JsonValue);
            return Ok(response);
        }

        #endregion
    }
}
