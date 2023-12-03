using Microsoft.AspNetCore.Mvc;
using vn.com.pnsuite.common.models;
using vn.com.pnsuite.toankhoan.dataaccess.interfaces;
using vn.com.pnsuite.toankhoan.Helpers;
using vn.com.pnsuite.toankhoan.models.user;

namespace vn.com.pnsuite.toankhoan.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : BaseApiController
    {
        public UserController(IUserService userService) : base(userService)
        {

        }



        [HttpGet("get-user-info")]
        public IActionResult GetUserInfo()
        {
            var response = _userService.GetById(CurrentUser.Id);
            return Ok(response);
        }

        [HttpPost("get-all")]
        public IActionResult getUsers(CommonRequest request)
        {
            if (this._userService.IsFunctionRight(DoNotCheckPermission, "USER_MANAGEMENT", CurrentUser.Id))
            {
                var response = _userService.GetAllByTaxcode(CurrentUser.Id, request.Taxcode, request.GetRequestValue("Search") == null ? null : request.GetRequestValue("Search").Value.ToString());
                return Ok(response);
            }
            else
            {
                var response = new ResponseData();
                response.ActionResult = ActionResultData.Failed;
                response.ErrorData = new ErrorDataModel() { ErrorCode = "NO_PERMISION", ErrorMessage = "Bạn không có quyền duyệt danh sách người dùng" };
                return Ok(response);
            }
        }
        [HttpPost("create")]
        public IActionResult Create([FromBody] UserModel value)
        {
            if (value.Id > 0)
            {
                if (this._userService.IsFunctionRight(DoNotCheckPermission, "EDIT_USER", CurrentUser.Id))
                {
                    var create = _userService.Create(value, CurrentUser.Id);
                    return Ok(create);
                }
                else
                {
                    var response = new ResponseData();
                    response.ActionResult = ActionResultData.Failed;
                    response.ErrorData = new ErrorDataModel() { ErrorCode = "NO_PERMISION", ErrorMessage = "Bạn không có quyền sửa người dùng" };
                    return Ok(response);
                }
            }
            else
            {
                if (this._userService.IsFunctionRight(DoNotCheckPermission, "ADD_USER", CurrentUser.Id))
                {
                    var create = _userService.Create(value, CurrentUser.Id);
                    return Ok(create);
                }
                else
                {
                    var response = new ResponseData();
                    response.ActionResult = ActionResultData.Failed;
                    response.ErrorData = new ErrorDataModel() { ErrorCode = "NO_PERMISION", ErrorMessage = "Bạn không có quyền thêm mới người dùng" };
                    return Ok(response);
                }
            }

        }
        [HttpPost("lock")]
        public IActionResult Lock([FromBody] UserModel value)
        {
            if (this._userService.IsFunctionRight(DoNotCheckPermission, "LOCK_USER", CurrentUser.Id))
            {
                var locked = _userService.Lock(value.Id, CurrentUser.Id);
                return Ok(locked);
            }
            else
            {
                var response = new ResponseData();
                response.ActionResult = ActionResultData.Failed;
                response.ErrorData = new ErrorDataModel() { ErrorCode = "NO_PERMISION", ErrorMessage = "Bạn không có quyền khóa người dùng" };
                return Ok(response);
            }

        }
        [HttpPost("unlock")]
        public IActionResult UnLock([FromBody] UserModel value)
        {
            if (this._userService.IsFunctionRight(DoNotCheckPermission, "UNLOCK_USER", CurrentUser.Id))
            {
                var unlocked = _userService.UnLock(value.Id, CurrentUser.Id);
                return Ok(unlocked);
            }
            else
            {
                var response = new ResponseData();
                response.ActionResult = ActionResultData.Failed;
                response.ErrorData = new ErrorDataModel() { ErrorCode = "NO_PERMISION", ErrorMessage = "Bạn không có quyền mở khóa người dùng" };
                return Ok(response);
            }

        }
        [HttpPost("reset")]
        public IActionResult Reset([FromBody] UserInputModel value)
        {
            if (this._userService.IsFunctionRight(DoNotCheckPermission, "RECREATE_PASSWORD", CurrentUser.Id))
            {
                var reset = _userService.Reset(value, CurrentUser.Id);
                return Ok(reset);
            }
            else
            {
                var response = new ResponseData();
                response.ActionResult = ActionResultData.Failed;
                response.ErrorData = new ErrorDataModel() { ErrorCode = "NO_PERMISION", ErrorMessage = "Bạn không có quyền đặt lại mật khẩu" };
                return Ok(response);
            }

        }
        [HttpPost("change-password")]
        public IActionResult changePassword([FromBody] UserChangePasswordModel value)
        {
            var reset = _userService.ChangePassword(value, CurrentUser.Id);
            return Ok(reset);
        }
        [HttpPost("delete")]
        public IActionResult Delete([FromBody] UserModel value)
        {
            if (this._userService.IsFunctionRight(DoNotCheckPermission, "DEL_USER", CurrentUser.Id))
            {
                var delete = _userService.Delete(value.Id, CurrentUser.Id);
                return Ok(delete);
            }
            else
            {
                var response = new ResponseData();
                response.ActionResult = ActionResultData.Failed;
                response.ErrorData = new ErrorDataModel() { ErrorCode = "NO_PERMISION", ErrorMessage = "Bạn không có quyền xóa người dùng" };
                return Ok(response);
            }

        }
    }
}
