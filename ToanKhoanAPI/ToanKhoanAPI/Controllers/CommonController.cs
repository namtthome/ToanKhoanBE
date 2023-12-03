using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using vn.com.pnsuite.common.models;
using vn.com.pnsuite.toankhoan.dataaccess.interfaces;
using vn.com.pnsuite.toankhoan.Helpers;
using vn.com.pnsuite.toankhoan.models.common;

namespace vn.com.pnsuite.toankhoan.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CommonController : BaseApiController
    {
        private readonly ICommonService _commonService;
        public CommonController(ICommonService commonService, IUserService userService) : base(userService)
        {
            _commonService = commonService;
        }
        [HttpGet("get-by-id/{Id}")]
        public IActionResult get(int id)
        {
            var common = _commonService.getById(id);
            return Ok(common);
        }
        [HttpPost("create")]
        public IActionResult create(ActionCreateUpdateModel data)
        {
            var response = new ResponseData();
            response.ActionResult = ActionResultData.Failed;
            response.ErrorData = new ErrorDataModel() { ErrorCode = "NO_PERMISION", ErrorMessage = "Bạn không có quyền sử dụng chức năng" };

            int _splash = data.Action.IndexOf("_");
            String _commonTypeCode = _splash == -1 ? data.Action : data.Action.Substring(_splash + 1);
            var checkData = _commonService.getTypeByCode(_commonTypeCode);

            if (checkData.ActionResult == ActionResultData.Failed)
            {
                return Ok(checkData);
            } else
            {
                CommonType type = (CommonType)checkData.ActionData;
                if (data.Action.StartsWith("EDIT"))
                {
                    if (this._userService.IsFunctionRight(DoNotCheckPermission, data.Action, CurrentUser.Id))
                    {
                        var create = _commonService.create(data.JsonData.ToString(), CurrentUser.Id);
                        return Ok(create);
                    }
                    else
                    {
                        response.ErrorData.ErrorMessage = "Bạn không có quyền sửa " + type.Description.ToLower();
                        return Ok(response);
                    }
                }
                else if (data.Action.StartsWith("ADD"))
                {
                    if (this._userService.IsFunctionRight(DoNotCheckPermission, data.Action, CurrentUser.Id))
                    {
                        var create = _commonService.create(data.JsonData.ToString(), CurrentUser.Id);
                        return Ok(create);
                    }
                    else
                    {
                        response.ErrorData.ErrorMessage = "Bạn không có quyền thêm mới " + type.Description.ToLower();
                        return Ok(response);
                    }
                }
                else
                {
                    return Ok(response);
                }
            }
        }
        [HttpPost("delete/{Id}")]
        public IActionResult delete(int id)
        {
            var response = new ResponseData();
            response.ActionResult = ActionResultData.Failed;
            response.ErrorData = new ErrorDataModel() { ErrorCode = "NO_PERMISION", ErrorMessage = "Bạn không có quyền thực hiện chức năng" };
            
            var checkData = _commonService.getTypeByCommonId(id);

            if (checkData.ActionResult == ActionResultData.Failed)
            {
                return Ok(checkData);
            } else
            {
                CommonType type = (CommonType)checkData.ActionData;
                if (this._userService.IsFunctionRight(DoNotCheckPermission, "DEL_NATIONAL", CurrentUser.Id))
                {
                    var delete = _commonService.delete(id, CurrentUser.Id);
                    return Ok(delete);
                }
                else
                {
                    response.ErrorData.ErrorMessage = "Bạn không có quyền xóa " + type.Description.ToLower();
                    return Ok(response);
                }
            }
        }
        [HttpPost("get-list")]
        public IActionResult list(CommonRequest request)
        {
            String _typeCode = request.GetRequestValue("Type").Value.ToString();
            var checkData = _commonService.getTypeByCode(_typeCode);

            if (checkData.ActionResult == ActionResultData.Failed)
            {
                return Ok(checkData);
            } else
            {
                CommonType type = (CommonType)checkData.ActionData;
                if (this._userService.IsFunctionRight(DoNotCheckPermission, request.Action, CurrentUser.Id))
                {
                    var response = _commonService.getAllByTaxcode(request);
                    return Ok(response);
                }
                else
                {
                    var response = new ResponseData();
                    response.ActionResult = ActionResultData.Failed;
                    response.ErrorData = new ErrorDataModel() { ErrorCode = "NO_PERMISION", ErrorMessage = "Bạn không có quyền duyệt danh sách " + type.Description.ToLower()};
                    return Ok(response);
                }
            }
        }
        [HttpPost("get-client-update-list")]
        public IActionResult GetUpdateList(CommonRequest request)
        {
            var response = _commonService.getClientFileNeedUpdate(this.CurrentUser.CompanyId, this.CurrentUser.Id, request.JsonValue);
            return Ok(response);
        }
        [HttpPost("remove-client-need-update")]
        public IActionResult RemoveClientNeedUpdate(CommonRequest request)
        {
            var response = _commonService.getClientFileNeedUpdate(this.CurrentUser.CompanyId, this.CurrentUser.Id, request.JsonValue);
            return Ok(response);
        }
        [HttpGet("get-file")]
        public IActionResult getFile(String fileName)
        {
            
            var path = Path.Combine(AppContext.BaseDirectory, $"Template\\{fileName}");
            MemoryStream file = new MemoryStream();
            using (FileStream fs = System.IO.File.OpenRead(path))
            {
                fs.CopyTo(file);
            }
            return File(file.ToArray(), "application/octet-stream", fileName);
        }
    }
}
