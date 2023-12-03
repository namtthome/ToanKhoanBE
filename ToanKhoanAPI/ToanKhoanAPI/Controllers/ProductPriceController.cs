using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Threading.Tasks;
using vn.com.pnsuite.common.models;
using vn.com.pnsuite.toankhoan.Controllers;
using vn.com.pnsuite.toankhoan.dataaccess.interfaces;
using vn.com.pnsuite.toankhoan.dataaccess.repositories;
using vn.com.pnsuite.toankhoan.Helpers;

namespace Pnsuite.ToanKhoan.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/product-price")]
    public class ProductPriceController : BaseApiController
    {
        private readonly IProductPriceService _productPriceService;

        public ProductPriceController(IUserService userService, IProductPriceService productPriceService) : base(userService)
        {
            _productPriceService = productPriceService;
        }
        [HttpPost("get-all")]
        public async Task<IActionResult> GetAllBySearch(CommonRequest request)
        {
            var response = await _productPriceService.GetAllSearchAsync(CurrentUser.CompanyId, CurrentUser.Id, request.JsonValue);
            return Ok(response);
        }
        [HttpPost("detail")]
        public async Task<IActionResult> GetDetailAsync(CommonRequest request)
        {
            var response = await _productPriceService.GetProductPriceById(CurrentUser.CompanyId, CurrentUser.Id, request.JsonValue);
            return Ok(response);
        }
        [HttpPost("create")]
        public async Task<IActionResult> CreateAsync([FromBody] dynamic request)
        {
            var response = await _productPriceService.CreateOrUpdateProductPriceAsync(CurrentUser.CompanyId, CurrentUser.Id, JsonSerializer.Serialize(request));
            return Ok(response);
        }
        [HttpPost("update")]
        public async Task<IActionResult> UpdateAsync([FromBody] dynamic request)
        {
            var response = await _productPriceService.CreateOrUpdateProductPriceAsync(CurrentUser.CompanyId, CurrentUser.Id, JsonSerializer.Serialize(request));
            return Ok(response);
        }
        [HttpPost("delete")]
        public async Task<IActionResult> Delete(CommonRequest request)
        {
            var response = await _productPriceService.DeleteProductPriceAsync(CurrentUser.CompanyId, CurrentUser.Id, request.JsonValue);
            return Ok(response);
        }
        [HttpPost("get-config-list")]
        public async Task<IActionResult> GetPriceConfigBySearch(CommonRequest request)
        {
            var response = await _productPriceService.GetPriceConfigSearchAsync(CurrentUser.CompanyId, CurrentUser.Id, request.JsonValue);
            return Ok(response);
        }
        [HttpPost("get-config-by-id")]
        public async Task<IActionResult> GetPriceConfigById(CommonRequest request)
        {
            var response = await _productPriceService.GetPriceConfigByIdAsync(CurrentUser.CompanyId, CurrentUser.Id, request.JsonValue);
            return Ok(response);
        }
        [HttpPost("create-config")]
        public async Task<IActionResult> CreatePriceConfigAsync([FromBody] dynamic request)
        {
            var response = await _productPriceService.InsertOrUpdatePriceConfigAsync(CurrentUser.CompanyId, CurrentUser.Id, JsonSerializer.Serialize(request));
            return Ok(response);
        }
        [HttpPost("update-config")]
        public async Task<IActionResult> UpdatePriceConfigAsync([FromBody] dynamic request)
        {
            var response = await _productPriceService.InsertOrUpdatePriceConfigAsync(CurrentUser.CompanyId, CurrentUser.Id, JsonSerializer.Serialize(request));
            return Ok(response);
        }
        [HttpPost("delete-config")]
        public async Task<IActionResult> DeleteConfig(CommonRequest request)
        {
            var response = await _productPriceService.DeletePriceConfigAsync(CurrentUser.CompanyId, CurrentUser.Id, request.JsonValue);
            return Ok(response);
        }
        [HttpPost("get-config-type-list")]
        public async Task<IActionResult> GetPriceConfigTypeBySearch(CommonRequest request)
        {
            var response = await _productPriceService.GetPriceConfigTypeSearchAsync(CurrentUser.CompanyId, CurrentUser.Id, request.JsonValue);
            return Ok(response);
        }
        [HttpPost("create-price-by-group")]
        public async Task<IActionResult> CreatePriceByGroup([FromBody] CommonRequest request)
        {
            var response = await _productPriceService.CreatePriceByConfigTypeAsync(this.CurrentUser.CompanyId, this.CurrentUser.Id, request.GetRequestValue("GroupCode").Value.ToString(), request.JsonValue);
            return Ok(response);
        }
    }
}
