using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Threading.Tasks;
using vn.com.pnsuite.common.models;
using vn.com.pnsuite.toankhoan.Controllers;
using vn.com.pnsuite.toankhoan.dataaccess.interfaces;
using vn.com.pnsuite.toankhoan.dataaccess.Interfaces.Categories;
using vn.com.pnsuite.toankhoan.Helpers;

namespace Pnsuite.ToanKhoan.Controllers.Categories
{
    [Authorize]
    [ApiController]
    [Route("api/cost")]
    public class CategoryCostController : BaseApiController
    {
        private readonly ICategoryCostService _categoryCostService;

        public CategoryCostController(IUserService userService, ICategoryCostService categoryCostService) : base(userService)
        {
            _categoryCostService = categoryCostService;
        }
        [HttpPost("get-all")]
        public async Task<IActionResult> GetAllBySearch(CommonRequest request)
        {
            var response = await _categoryCostService.GetAllBySearchAsync(CurrentUser.CompanyId, CurrentUser.Id, request.JsonValue);
            return Ok(response);
        }
        [HttpPost("detail")]
        public async Task<IActionResult> GetDetailAsync(CommonRequest request)
        {
            var response = await _categoryCostService.GetDetail(CurrentUser.CompanyId, CurrentUser.Id, request.JsonValue);
            return Ok(response);
        }
        [HttpPost("create")]
        public async Task<IActionResult> CreateAsync([FromBody] dynamic request)
        {
            var response = await _categoryCostService.CreateAsync(CurrentUser.CompanyId, CurrentUser.Id, JsonSerializer.Serialize(request));
            return Ok(response);
        }
        [HttpPost("update")]
        public async Task<IActionResult> UpdateAsync([FromBody] dynamic request)
        {
            var response = await _categoryCostService.UpdateAsync(CurrentUser.CompanyId, CurrentUser.Id, JsonSerializer.Serialize(request));
            return Ok(response);
        }
        [HttpPost("delete")]
        public async Task<IActionResult> Delete(CommonRequest request)
        {
            var response = await _categoryCostService.Delete(CurrentUser.CompanyId, CurrentUser.Id, request.JsonValue);
            return Ok(response);
        }
    }
}
