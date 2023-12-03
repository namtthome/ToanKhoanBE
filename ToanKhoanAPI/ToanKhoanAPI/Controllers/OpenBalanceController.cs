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
    [Route("api/open-balance")]
    public class OpenBalanceController : BaseApiController
    {
        private readonly IOpenBalanceService _openBalanceService;
        public OpenBalanceController(IOpenBalanceService openBalanceService, IUserService userService) : base(userService)
        {
            _openBalanceService = openBalanceService;
        }
        [HttpPost("debt-list")]
        public async Task<IActionResult> GetDebtList([FromBody] CommonRequest request)
        {
            var response = await _openBalanceService.GetDebtOpenBalanceList(this.CurrentUser.CompanyId, this.CurrentUser.Id, request.Values == null ? "[]" : JsonSerializer.Serialize(request.Values));
            return Ok(response);
        }
        [HttpPost("get-debt-by-id")]
        public async Task<IActionResult> GetDebtById([FromBody] CommonRequest request)
        {
            var response = await _openBalanceService.GetDetailDebtOpenBalance(this.CurrentUser.CompanyId, this.CurrentUser.Id, JsonSerializer.Serialize(request.Values));
            return Ok(response);
        }
        [HttpPost("delete-debt")]
        public async Task<IActionResult> DeleteDebt([FromBody] CommonRequest request)
        {
            var response = await _openBalanceService.DeleteDebtOpenBalance(this.CurrentUser.CompanyId, this.CurrentUser.Id, JsonSerializer.Serialize(request.Values));
            return Ok(response);
        }
        [HttpPost("create-debt")]
        public async Task<IActionResult> CreateDebt([FromBody] dynamic request)
        {
            var response = await _openBalanceService.CreateOrUpdateDebtOpenBalance(this.CurrentUser.CompanyId, this.CurrentUser.Id, JsonSerializer.Serialize(request));
            return Ok(response);
        }
        [HttpPost("update-debt")]
        public async Task<IActionResult> UpdateDebt([FromBody] dynamic request)
        {
            var response = await _openBalanceService.CreateOrUpdateDebtOpenBalance(this.CurrentUser.CompanyId, this.CurrentUser.Id, JsonSerializer.Serialize(request));
            return Ok(response);
        }
        [HttpPost("inventory-list")]
        public async Task<IActionResult> GetInventoryList([FromBody] CommonRequest request)
        {
            var response = await _openBalanceService.GetInventoryOpenBalanceList(this.CurrentUser.CompanyId, this.CurrentUser.Id, request.Values == null ? "[]" : JsonSerializer.Serialize(request.Values));
            return Ok(response);
        }
        [HttpPost("get-inventory-by-id")]
        public async Task<IActionResult> GetInventoryById([FromBody] CommonRequest request)
        {
            var response = await _openBalanceService.GetDetailInventoryOpenBalance(this.CurrentUser.CompanyId, this.CurrentUser.Id, JsonSerializer.Serialize(request.Values));
            return Ok(response);
        }
        [HttpPost("delete-inventory")]
        public async Task<IActionResult> DeleteInventory([FromBody] CommonRequest request)
        {
            var response = await _openBalanceService.DeleteInventoryOpenBalance(this.CurrentUser.CompanyId, this.CurrentUser.Id, JsonSerializer.Serialize(request.Values));
            return Ok(response);
        }
        [HttpPost("create-inventory")]
        public async Task<IActionResult> CreateInventory([FromBody] dynamic request)
        {
            var response = await _openBalanceService.CreateOrUpdateInventoryOpenBalance(this.CurrentUser.CompanyId, this.CurrentUser.Id, JsonSerializer.Serialize(request));
            return Ok(response);
        }
        [HttpPost("update-inventory")]
        public async Task<IActionResult> UpdateInventory([FromBody] dynamic request)
        {
            var response = await _openBalanceService.CreateOrUpdateInventoryOpenBalance(this.CurrentUser.CompanyId, this.CurrentUser.Id, JsonSerializer.Serialize(request));
            return Ok(response);
        }
    }
}
