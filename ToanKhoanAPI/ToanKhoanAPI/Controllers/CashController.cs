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
    public class CashController : BaseApiController
    {
        private readonly ICashService _cashService;

        public CashController(IUserService userService, ICashService cashService) : base(userService)
        {
            _cashService = cashService;
        }

        #region Cash Receive
        [HttpPost("receive-list")]
        public async Task<IActionResult> GetReceiveList([FromBody] CommonRequest request)
        {
            var response = _cashService.GetReceiveList(this.CurrentUser.CompanyId, this.CurrentUser.Id, request.JsonValue);
            return Ok(response);
        }
        [HttpPost("get-receive-by-id")]
        public async Task<IActionResult> GetReceiveById([FromBody] CommonRequest request)
        {
            var response = _cashService.GetReceiveById(this.CurrentUser.CompanyId, this.CurrentUser.Id, request.JsonValue);
            return Ok(response);
        }
        [HttpPost("delete-receive")]
        public async Task<IActionResult> DeleteReceive([FromBody] CommonRequest request)
        {
            var response = _cashService.DeleteReceiveById(this.CurrentUser.CompanyId, this.CurrentUser.Id, request.JsonValue);
            return Ok(response);
        }
        [HttpPost("create-receive")]
        public async Task<IActionResult> CreateReceive([FromBody] dynamic request)
        {
            var response = _cashService.UpdateReceive(this.CurrentUser.CompanyId, this.CurrentUser.Id, JsonSerializer.Serialize(request));
            return Ok(response);
        }
        [HttpPost("update-receive")]
        public async Task<IActionResult> UpdateReceive([FromBody] dynamic request)
        {
            var response = _cashService.UpdateReceive(this.CurrentUser.CompanyId, this.CurrentUser.Id, JsonSerializer.Serialize(request));
            return Ok(response);
        }
        #endregion

        #region Cash Payment
        [HttpPost("payment-list")]
        public async Task<IActionResult> GetPaymentList([FromBody] CommonRequest request)
        {
            var response = _cashService.GetPaymentList(this.CurrentUser.CompanyId, this.CurrentUser.Id, request.JsonValue);
            return Ok(response);
        }
        [HttpPost("get-payment-by-id")]
        public async Task<IActionResult> GetPaymentById([FromBody] CommonRequest request)
        {
            var response = _cashService.GetPaymentById(this.CurrentUser.CompanyId, this.CurrentUser.Id, request.JsonValue);
            return Ok(response);
        }
        [HttpPost("delete-payment")]
        public async Task<IActionResult> DeletePayment([FromBody] CommonRequest request)
        {
            var response = _cashService.DeletePaymentById(this.CurrentUser.CompanyId, this.CurrentUser.Id, request.JsonValue);
            return Ok(response);
        }
        [HttpPost("create-payment")]
        public async Task<IActionResult> CreatePayment([FromBody] dynamic request)
        {
            var response = _cashService.UpdatePayment(this.CurrentUser.CompanyId, this.CurrentUser.Id, JsonSerializer.Serialize(request));
            return Ok(response);
        }
        [HttpPost("update-payment")]
        public async Task<IActionResult> UpdatePayment([FromBody] dynamic request)
        {
            var response = _cashService.UpdatePayment(this.CurrentUser.CompanyId, this.CurrentUser.Id, JsonSerializer.Serialize(request));
            return Ok(response);
        }
        #endregion
    }
}
