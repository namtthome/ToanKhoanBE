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
    [Route("api/[controller]")]
    public class WarehouseController : BaseApiController
    {
        private readonly IWarehouseService _warehouseService;
        public WarehouseController(IWarehouseService warehouseService, IUserService userService) : base(userService)
        {
            _warehouseService = warehouseService;
        }
        #region warehouse input
        [HttpPost("warehouse-input-list")]
        public async Task<IActionResult> GetWarehouseInputList([FromBody] CommonRequest request)
        {
            var response = _warehouseService.GetWarehouseInputList(this.CurrentUser.CompanyId, this.CurrentUser.Id, request.Values == null ? "[]" : JsonSerializer.Serialize(request.Values));
            return Ok(response);
        }
        [HttpPost("detail-warehouse-input")]
        public async Task<IActionResult> GetWarehouseInputDetail(CommonRequest request)
        {
            var response = _warehouseService.GetWarehouseInputById(CurrentUser.CompanyId, CurrentUser.Id, request.JsonValue);
            return Ok(response);
        }
        [HttpPost("create-warehouse-input")]
        public async Task<IActionResult> CreateWarehouseInput([FromBody] dynamic request)
        {
            var response = _warehouseService.UpdateWarehouseInput(this.CurrentUser.CompanyId, this.CurrentUser.Id, JsonSerializer.Serialize(request));
            return Ok(response);
        }
        [HttpPost("update-warehouse-input")]
        public async Task<IActionResult> UpdateWarehouseInput([FromBody] dynamic request)
        {
            var response = _warehouseService.UpdateWarehouseInput(this.CurrentUser.CompanyId, this.CurrentUser.Id, JsonSerializer.Serialize(request));
            return Ok(response);
        }
        [HttpPost("delete-warehouse-input")]
        public async Task<IActionResult> DeleteWarehouseInput([FromBody] CommonRequest request)
        {
            var response = _warehouseService.DeleteWarehouseInputById(this.CurrentUser.CompanyId, this.CurrentUser.Id, JsonSerializer.Serialize(request.Values));
            return Ok(response);
        }
        #endregion

        #region warehouse output
        [HttpPost("warehouse-output-list")]
        public async Task<IActionResult> GetWarehouseOutputList([FromBody] CommonRequest request)
        {
            var response = _warehouseService.GetWarehouseOutputList(this.CurrentUser.CompanyId, this.CurrentUser.Id, request.Values == null ? "[]" : JsonSerializer.Serialize(request.Values));
            return Ok(response);
        }
        [HttpPost("detail-warehouse-output")]
        public async Task<IActionResult> GetWarehouseOutputDetail(CommonRequest request)
        {
            var response = _warehouseService.GetWarehouseOutputById(CurrentUser.CompanyId, CurrentUser.Id, request.JsonValue);
            return Ok(response);
        }
        [HttpPost("create-warehouse-output")]
        public async Task<IActionResult> CreateWarehouseOutput([FromBody] dynamic request)
        {
            var response = _warehouseService.UpdateWarehouseOutput(this.CurrentUser.CompanyId, this.CurrentUser.Id, JsonSerializer.Serialize(request));
            return Ok(response);
        }
        [HttpPost("update-warehouse-output")]
        public async Task<IActionResult> UpdateWarehouseOutput([FromBody] dynamic request)
        {
            var response = _warehouseService.UpdateWarehouseOutput(this.CurrentUser.CompanyId, this.CurrentUser.Id, JsonSerializer.Serialize(request));
            return Ok(response);
        }
        [HttpPost("delete-warehouse-output")]
        public async Task<IActionResult> DeleteWarehouseOutput([FromBody] CommonRequest request)
        {
            var response = _warehouseService.DeleteWarehouseOutputById(this.CurrentUser.CompanyId, this.CurrentUser.Id, JsonSerializer.Serialize(request.Values));
            return Ok(response);
        }
        #endregion

        #region warehouse delivery
        [HttpPost("warehouse-delivery-list")]
        public async Task<IActionResult> GetWarehouseDeliveryList([FromBody] CommonRequest request)
        {
            var response = _warehouseService.GetWarehouseDeliveryList(this.CurrentUser.CompanyId, this.CurrentUser.Id, request.Values == null ? "[]" : JsonSerializer.Serialize(request.Values));
            return Ok(response);
        }
        [HttpPost("detail-warehouse-delivery")]
        public async Task<IActionResult> GetWarehouseDeliveryDetail(CommonRequest request)
        {
            var response = _warehouseService.GetWarehouseDeliveryById(CurrentUser.CompanyId, CurrentUser.Id, request.JsonValue);
            return Ok(response);
        }
        [HttpPost("create-warehouse-delivery")]
        public async Task<IActionResult> CreateWarehouseDelivery([FromBody] dynamic request)
        {
            var response = _warehouseService.UpdateWarehouseDelivery(this.CurrentUser.CompanyId, this.CurrentUser.Id, JsonSerializer.Serialize(request));
            return Ok(response);
        }
        [HttpPost("update-warehouse-delivery")]
        public async Task<IActionResult> UpdateWarehouseDelivery([FromBody] dynamic request)
        {
            var response = _warehouseService.UpdateWarehouseDelivery(this.CurrentUser.CompanyId, this.CurrentUser.Id, JsonSerializer.Serialize(request));
            return Ok(response);
        }
        [HttpPost("delete-warehouse-delivery")]
        public async Task<IActionResult> DeleteWarehouseDelivery([FromBody] CommonRequest request)
        {
            var response = _warehouseService.DeleteWarehouseDeliveryById(this.CurrentUser.CompanyId, this.CurrentUser.Id, JsonSerializer.Serialize(request.Values));
            return Ok(response);
        }
        #endregion
    }
}
