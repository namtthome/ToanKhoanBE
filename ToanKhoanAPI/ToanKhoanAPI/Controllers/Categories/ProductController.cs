using DocumentFormat.OpenXml.Office2016.Excel;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Threading.Tasks;
using vn.com.pnsuite.common.models;
using vn.com.pnsuite.toankhoan.Controllers;
using vn.com.pnsuite.toankhoan.dataaccess.interfaces;
using vn.com.pnsuite.toankhoan.dataaccess.Interfaces.Categories;
using vn.com.pnsuite.toankhoan.Helpers;
using vn.com.pnsuite.toankhoan.models.Products.DTOs;

namespace Pnsuite.ToanKhoan.Controllers.Categories
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : BaseApiController
    {
        private readonly IProductService _productService;

        public ProductController(IUserService userService, IProductService productService) : base(userService)
        {
            _productService = productService;
        }

        [HttpPost("get-all")]
        public async Task<IActionResult> GetAllBySearch(CommonRequest request)
        {
            var response = await _productService.GetAllBySearchAsync(CurrentUser.CompanyId, CurrentUser.Id, request.JsonValue);
            return Ok(response);
        }
        [HttpPost("get-all-inventory")]
        public async Task<IActionResult> GetAllInventoryBySearch(CommonRequest request)
        {
            var response = await _productService.GetAllInventoryBySearch(CurrentUser.CompanyId, CurrentUser.Id, request.JsonValue);
            return Ok(response);
        }
        [HttpPost("create")]
        public async Task<IActionResult> CreateAsync([FromBody] dynamic request)
        {
            var response = await _productService.CreateOrUpdateAsync(CurrentUser.CompanyId, CurrentUser.Id, JsonSerializer.Serialize(request));
            return Ok(response);
        }
        [HttpPost("detail")]  
        public async Task<IActionResult> GetDetail(CommonRequest request)
        {
            var response = await _productService.GetDetail(CurrentUser.CompanyId, CurrentUser.Id, request.JsonValue);
            return Ok(response);
        }
        [HttpPost("copy")]
        public async Task<IActionResult> GetCopyData(CommonRequest request)
        {
            var response = await _productService.GetCopyData(CurrentUser.CompanyId, CurrentUser.Id, request.JsonValue);
            return Ok(response);
        }
        [HttpPost("delete")]
        public async Task<IActionResult> Delete(CommonRequest request)
        {
            var response = await _productService.Delete(CurrentUser.CompanyId, CurrentUser.Id, request.JsonValue);
            return Ok(response);
        }
        [HttpPost("update")]
        public async Task<IActionResult> UpdateAsync([FromBody] dynamic request)
        {
            var response = await _productService.CreateOrUpdateAsync(CurrentUser.CompanyId, CurrentUser.Id, JsonSerializer.Serialize(request));
            return Ok(response);
        }
        [HttpGet("get-extra-info")]
        public async Task<IActionResult> GetExtraInfo(CommonRequest request)
        {
            var response = await _productService.GetExtraInfo(CurrentUser.CompanyId, CurrentUser.Id, request.JsonValue);
            return Ok(response);
        }
        [HttpPost("get-price")]
        public async Task<IActionResult> GetPrice(CommonRequest request)
        {
            var response = await _productService.GetPrice(CurrentUser.CompanyId, CurrentUser.Id, request.JsonValue);
            return Ok(response);
        }
    }
}
