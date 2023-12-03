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
    [Route("api/[controller]")]
    public class PricebookController : BaseApiController
    {
        private readonly IPricebookService _pricebookService;

        public PricebookController(IUserService userService, IPricebookService pricebookService) : base(userService)
        {
            _pricebookService = pricebookService;
        }

        [HttpPost("get-all")]
        public async Task<IActionResult> GetAllBySearch(CommonRequest request)
        {
            var response = await _pricebookService.GetAllBySearchAsync(CurrentUser.CompanyId, CurrentUser.Id, request.JsonValue);
            return Ok(response);
        }

        [HttpPost("get-all-status")]
        public async Task<IActionResult> GetAllStatusBySearch(CommonRequest request)
        {
            var response = await _pricebookService.GetAllStatusBySearchAsync(CurrentUser.CompanyId, CurrentUser.Id, request.JsonValue);
            return Ok(response);
        }


        [HttpPost("create")]
        public async Task<IActionResult> CreateAsync([FromBody] dynamic request)
        {
            var response = await _pricebookService.CreateAsync(CurrentUser.CompanyId, CurrentUser.Id, JsonSerializer.Serialize(request));
            return Ok(response);
        }

        [HttpPost("detail")]  
        public async Task<IActionResult> GetDetail(CommonRequest request)
        {
            var response = await _pricebookService.GetDetail(CurrentUser.CompanyId, CurrentUser.Id, request.JsonValue);
            return Ok(response);
        }

        [HttpPost("delete")]
        public async Task<IActionResult> Delete(CommonRequest request)
        {
            var response = await _pricebookService.Delete(CurrentUser.CompanyId, CurrentUser.Id, request.JsonValue);
            return Ok(response);
        }


        [HttpPost("update")]
        public async Task<IActionResult> UpdateAsync([FromBody] dynamic request)
        {
            var response = await _pricebookService.UpdateAsync(CurrentUser.CompanyId, CurrentUser.Id, JsonSerializer.Serialize(request));
            return Ok(response);
        }

        [HttpPost("detail/add-or-update")]
        public async Task<IActionResult> AddOrUpdatePricebookDetail([FromBody] dynamic request)
        {
            var response = await _pricebookService.AddOrUpdatePricebookDetail(CurrentUser.CompanyId, CurrentUser.Id, JsonSerializer.Serialize(request));
            return Ok(response);
        }

        [HttpPost("detail/delete-detail")]
        public async Task<IActionResult> DeleteDetail([FromBody] dynamic request)
        {
            var response = await _pricebookService.DeletePricebookDetail(CurrentUser.CompanyId, CurrentUser.Id, JsonSerializer.Serialize(request));
            return Ok(response);
        }
    }
}
