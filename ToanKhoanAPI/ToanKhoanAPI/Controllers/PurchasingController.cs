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
    public class PurchasingController : BaseApiController
    {
        private readonly IPurchasingService _purchasingService;
        public PurchasingController(IPurchasingService purchasingService, IUserService userService) : base(userService)
        {
            _purchasingService = purchasingService;
        }
        #region order
        [HttpPost("order-list")]
        public async Task<IActionResult> GetOrderList([FromBody] CommonRequest request)
        {
            var response = _purchasingService.GetOrderList(this.CurrentUser.CompanyId, this.CurrentUser.Id, request.Values == null ? "[]" : JsonSerializer.Serialize(request.Values));
            return Ok(response);
        }
        [HttpPost("get-order-by-id")]
        public async Task<IActionResult> GetOrderById([FromBody] CommonRequest request)
        {
            var response = _purchasingService.GetOrderById(this.CurrentUser.CompanyId, this.CurrentUser.Id, JsonSerializer.Serialize(request.Values));
            return Ok(response);
        }
        [HttpPost("delete-order")]
        public async Task<IActionResult> DeleteOrder([FromBody] CommonRequest request)
        {
            var response = _purchasingService.DeleteOrderById(this.CurrentUser.CompanyId, this.CurrentUser.Id, JsonSerializer.Serialize(request.Values));
            return Ok(response);
        }
        [HttpPost("create-order")]
        public async Task<IActionResult> CreateOrder([FromBody] dynamic request)
        {
            var response = _purchasingService.UpdateOrder(this.CurrentUser.CompanyId, this.CurrentUser.Id, JsonSerializer.Serialize(request));
            return Ok(response);
        }
        [HttpPost("update-order")]
        public async Task<IActionResult> UpdateOrder([FromBody] dynamic request)
        {
            var response = _purchasingService.UpdateOrder(this.CurrentUser.CompanyId, this.CurrentUser.Id, JsonSerializer.Serialize(request));
            return Ok(response);
        }
        #endregion
        
        #region voucher
        [HttpPost("voucher-list")]
        public async Task<IActionResult> GetVoucherList([FromBody] CommonRequest request)
        {
            var response = _purchasingService.GetVoucherList(this.CurrentUser.CompanyId, this.CurrentUser.Id, request.Values == null ? "[]" : JsonSerializer.Serialize(request.Values));
            return Ok(response);
        }
        [HttpPost("get-voucher-by-id")]
        public async Task<IActionResult> GetVoucherById([FromBody] CommonRequest request)
        {
            var response = _purchasingService.GetVoucherById(this.CurrentUser.CompanyId, this.CurrentUser.Id, JsonSerializer.Serialize(request.Values));
            return Ok(response);
        }
        [HttpPost("copy-voucher")]
        public async Task<IActionResult> GetCopyVoucher([FromBody] CommonRequest request)
        {
            var response = _purchasingService.GetCopyVoucher(this.CurrentUser.CompanyId, this.CurrentUser.Id, JsonSerializer.Serialize(request.Values));
            return Ok(response);
        }
        [HttpPost("create-voucher")]
        public async Task<IActionResult> CreateVoucher([FromBody] dynamic request)
        {
            var response = _purchasingService.UpdateVoucher(this.CurrentUser.CompanyId, this.CurrentUser.Id, JsonSerializer.Serialize(request));
            return Ok(response);
        }
        [HttpPost("update-voucher")]
        public async Task<IActionResult> UpdateVoucher([FromBody] dynamic request)
        {
            var response = _purchasingService.UpdateVoucher(this.CurrentUser.CompanyId, this.CurrentUser.Id, JsonSerializer.Serialize(request));
            return Ok(response);
        }
        [HttpPost("delete-voucher")]
        public async Task<IActionResult> DeleteVoucher([FromBody] CommonRequest request)
        {
            var response = _purchasingService.DeleteVoucherById(this.CurrentUser.CompanyId, this.CurrentUser.Id, JsonSerializer.Serialize(request.Values));
            return Ok(response);
        }
        #endregion

        #region return
        [HttpPost("return-list")]
        public async Task<IActionResult> GetReturnList([FromBody] CommonRequest request)
        {
            var response = _purchasingService.GetReturnList(this.CurrentUser.CompanyId, this.CurrentUser.Id, request.Values == null ? "[]" : JsonSerializer.Serialize(request.Values));
            return Ok(response);
        }
        [HttpPost("get-return-by-id")]
        public async Task<IActionResult> GetReturnById([FromBody] CommonRequest request)
        {
            var response = _purchasingService.GetReturnById(this.CurrentUser.CompanyId, this.CurrentUser.Id, JsonSerializer.Serialize(request.Values));
            return Ok(response);
        }
        [HttpPost("get-copy-return")]
        public async Task<IActionResult> GetCopyDataReturnById([FromBody] CommonRequest request)
        {
            var response = _purchasingService.GetCopyDataReturnById(this.CurrentUser.CompanyId, this.CurrentUser.Id, JsonSerializer.Serialize(request.Values));
            return Ok(response);
        }
        [HttpPost("create-return")]
        public async Task<IActionResult> CreateReturn([FromBody] dynamic request)
        {
            var response = _purchasingService.UpdateReturn(this.CurrentUser.CompanyId, this.CurrentUser.Id, JsonSerializer.Serialize(request));
            return Ok(response);
        }
        [HttpPost("update-return")]
        public async Task<IActionResult> UpdateReturn([FromBody] dynamic request)
        {
            var response = _purchasingService.UpdateReturn(this.CurrentUser.CompanyId, this.CurrentUser.Id, JsonSerializer.Serialize(request));
            return Ok(response);
        }
        [HttpPost("delete-return")]
        public async Task<IActionResult> DeleteReturn([FromBody] CommonRequest request)
        {
            var response = _purchasingService.DeleteReturnById(this.CurrentUser.CompanyId, this.CurrentUser.Id, JsonSerializer.Serialize(request.Values));
            return Ok(response);
        }
        #endregion
    }
}
