using System;
using System.Collections.Generic;
using System.Text;
using vn.com.pnsuite.common.models;
using vn.com.pnsuite.toankhoan.models.function;

namespace vn.com.pnsuite.toankhoan.dataaccess.interfaces
{
    public interface IFunctionService
    {
        ResponseData GetFunctionByUser(long userId);
        ResponseData GetPermissionByUser(long userId);
        ResponseData SaveFunctionByUser(List<FunctionModel> functions, long userId, long saveUserId);
        ResponseData SavePermissionByUser(ActionCreateUpdateModel model, long saveUserId);
    }
}
