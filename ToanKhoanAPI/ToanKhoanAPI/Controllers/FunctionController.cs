using Microsoft.AspNetCore.Mvc;
using vn.com.pnsuite.common.models;
using vn.com.pnsuite.toankhoan.dataaccess.interfaces;
using vn.com.pnsuite.toankhoan.Helpers;
using vn.com.pnsuite.toankhoan.models.function;

namespace vn.com.pnsuite.toankhoan.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class FunctionController : BaseApiController
    {
        private readonly IFunctionService _functionService;
        public FunctionController(IFunctionService functionService, IUserService userService) : base(userService)
        {
            _functionService = functionService;
        }
        [HttpGet("get-by-active-user")]
        public IActionResult getByActiveUser()
        {
            var response = _functionService.GetFunctionByUser(CurrentUser.Id);
            return Ok(response);
        }
        [HttpGet("get-by-user")]
        public IActionResult getByUser(int userId)
        {
            var response = _functionService.GetFunctionByUser(userId);
            return Ok(response);
        }

        [HttpPost("save-user-function")]
        public IActionResult saveUserFunction([FromBody] SaveUserFunctionModel value)
        {
            
            if (this._userService.IsFunctionRight(DoNotCheckPermission, "SET_USER_RIGHT", CurrentUser.Id))
            {
                var locked = _functionService.SaveFunctionByUser(value.Functions, value.UserId, CurrentUser.Id);
                return Ok(locked);
            }
            else
            {
                var response = new ResponseData();
                response.ActionResult = ActionResultData.Failed;
                response.ErrorData = new ErrorDataModel() { ErrorCode = "NO_PERMISION", ErrorMessage = "Bạn không có quyền phân quyền chức năng người dùng" };
                return Ok(response);
            }

        }
    }
}
