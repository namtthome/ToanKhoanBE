using Microsoft.AspNetCore.Mvc;
using vn.com.pnsuite.toankhoan.Controllers;
using vn.com.pnsuite.toankhoan.dataaccess.Interfaces.Categories;
using vn.com.pnsuite.toankhoan.dataaccess.interfaces;
using vn.com.pnsuite.toankhoan.Helpers;
using vn.com.pnsuite.toankhoan.dataaccess.Interfaces;
using System.Threading.Tasks;
using vn.com.pnsuite.common.models;
using vn.com.pnsuite.toankhoan.dataaccess.Repositories;
using System.Text.Json;

namespace Pnsuite.ToanKhoan.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class BankController : BaseApiController
    {
        private readonly IBankService _bankService;

        public BankController(IUserService userService, IBankService bankService) : base(userService)
        {
            _bankService = bankService;
        }

        #region Bank Receive
        [HttpPost("receive-list")]
        public async Task<IActionResult> GetReceiveList([FromBody] CommonRequest request)
        {
            var response = _bankService.GetReceiveList(this.CurrentUser.CompanyId, this.CurrentUser.Id, request.JsonValue);
            return Ok(response);
        }
        [HttpPost("get-receive-by-id")]
        public async Task<IActionResult> GetReceiveById([FromBody] CommonRequest request)
        {
            var response = _bankService.GetReceiveById(this.CurrentUser.CompanyId, this.CurrentUser.Id, request.JsonValue);
            return Ok(response);
        }
        [HttpPost("delete-receive")]
        public async Task<IActionResult> DeleteReceive([FromBody] CommonRequest request)
        {
            var response = _bankService.DeleteReceiveById(this.CurrentUser.CompanyId, this.CurrentUser.Id, request.JsonValue);
            return Ok(response);
        }
        [HttpPost("create-receive")]
        public async Task<IActionResult> CreateReceive([FromBody] dynamic request)
        {
            var response = _bankService.UpdateReceive(this.CurrentUser.CompanyId, this.CurrentUser.Id, JsonSerializer.Serialize(request));
            return Ok(response);
        }
        [HttpPost("update-receive")]
        public async Task<IActionResult> UpdateReceive([FromBody] dynamic request)
        {
            var response = _bankService.UpdateReceive(this.CurrentUser.CompanyId, this.CurrentUser.Id, JsonSerializer.Serialize(request));
            return Ok(response);
        }
        #endregion

        #region Bank Payment
        [HttpPost("payment-list")]
        public async Task<IActionResult> GetPaymentList([FromBody] CommonRequest request)
        {
            var response = _bankService.GetPaymentList(this.CurrentUser.CompanyId, this.CurrentUser.Id, request.JsonValue);
            return Ok(response);
        }
        [HttpPost("get-payment-by-id")]
        public async Task<IActionResult> GetPaymentById([FromBody] CommonRequest request)
        {
            var response = _bankService.GetPaymentById(this.CurrentUser.CompanyId, this.CurrentUser.Id, request.JsonValue);
            return Ok(response);
        }
        [HttpPost("delete-payment")]
        public async Task<IActionResult> DeletePayment([FromBody] CommonRequest request)
        {
            var response = _bankService.DeletePaymentById(this.CurrentUser.CompanyId, this.CurrentUser.Id, request.JsonValue);
            return Ok(response);
        }
        [HttpPost("create-payment")]
        public async Task<IActionResult> CreatePayment([FromBody] dynamic request)
        {
            var response = _bankService.UpdatePayment(this.CurrentUser.CompanyId, this.CurrentUser.Id, JsonSerializer.Serialize(request));
            return Ok(response);
        }
        [HttpPost("update-payment")]
        public async Task<IActionResult> UpdatePayment([FromBody] dynamic request)
        {
            var response = _bankService.UpdatePayment(this.CurrentUser.CompanyId, this.CurrentUser.Id, JsonSerializer.Serialize(request));
            return Ok(response);
        }
        #endregion
    }
}
