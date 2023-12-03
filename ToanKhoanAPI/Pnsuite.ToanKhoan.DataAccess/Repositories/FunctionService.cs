using Dapper;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using vn.com.pnsuite.common.dataaccess.interfaces;
using vn.com.pnsuite.common.models;
using vn.com.pnsuite.toankhoan.dataaccess.interfaces;
using vn.com.pnsuite.toankhoan.models.function;

namespace vn.com.pnsuite.toankhoan.dataaccess.repositories
{
    public class FunctionService : IFunctionService
    {
        private readonly IBaseService baseService;
        private readonly AppSettings appSettings;
        public FunctionService(IOptions<AppSettings> appSettings, IBaseService baseService)
        {
            this.appSettings = appSettings.Value;
            this.baseService = baseService;
        }
        public ResponseData GetFunctionByUser(long userId)
        {
            ResponseData response = new ResponseData();
            try
            {
                var param = new DynamicParameters();
                param.Add("@UserId", dbType: DbType.Int64, value: userId, direction: ParameterDirection.Input);
                response.ActionResult = ActionResultData.Success;
                response.ActionData = baseService.GetList<FunctionModel>("sp_Function_GetListByUser", param: param);
            }
            catch (Exception ex)
            {
                response.ActionResult = ActionResultData.Failed;
                response.ErrorData = new ErrorDataModel(ex);
            }
            return response;
        }
        public ResponseData SaveFunctionByUser(List<FunctionModel> functions, long userId, long saveUserId)
        {
            ResponseData response = new ResponseData();
            try
            {
                DataTable dataTable = new DataTable();
                dataTable.Columns.Add("UserId", typeof(Int64));
                dataTable.Columns.Add("FunctionId", typeof(Int32));
                dataTable.Columns.Add("Accessibly", typeof(Boolean));

                foreach (FunctionModel function in functions)
                {
                    dataTable.Rows.Add(userId, function.Id, function.Accessibly);
                }

                var param = new DynamicParameters();
                param.Add("@UserFunction", dbType: DbType.Object, value: dataTable, direction: ParameterDirection.Input);
                param.Add("@UserId", dbType: DbType.Int64, value: userId, direction: ParameterDirection.Input);
                param.Add("@SaveUserId", dbType: DbType.Int64, value: saveUserId, direction: ParameterDirection.Input);

                baseService.GetList<FunctionModel>("sp_Function_SaveUserFunctions", param: param);
                response.ActionResult = ActionResultData.Success;
            }
            catch (Exception ex)
            {
                response.ActionResult = ActionResultData.Failed;
                response.ErrorData = new ErrorDataModel(ex);
            }
            return response;
        }
        public ResponseData GetPermissionByUser(long userId)
        {
            ResponseData response = new ResponseData();
            try
            {
                var param = new DynamicParameters();
                param.Add("@UserId", dbType: DbType.Int64, value: userId, direction: ParameterDirection.Input);
                var result = baseService.Connection().QueryMultiple("dbo.sp_Permission_GetListByUser", param: param, commandType: CommandType.StoredProcedure);

                List<FunctionModel> menus = result.Read<FunctionModel>().AsList();
                List<FunctionModel> departments = result.Read<FunctionModel>().AsList();
                List<FunctionModel> positions = result.Read<FunctionModel>().AsList();

                response.ActionResult = ActionResultData.Success;
                response.ActionData = new { UserId = userId, Menus = menus, Departments = departments, Positions = positions };
            }
            catch (Exception ex)
            {
                response.ActionResult = ActionResultData.Failed;
                response.ErrorData = new ErrorDataModel(ex);
            }
            return response;
        }
        public ResponseData SavePermissionByUser(ActionCreateUpdateModel model, long saveUserId)
        {
            ResponseData response = new ResponseData();
            try
            {
                var param = new DynamicParameters();
                String jsonData = model.JsonData.ToString();
                param.Add("@PermissionData", dbType: DbType.String, value: jsonData, direction: ParameterDirection.Input);
                param.Add("@SaveUserId", dbType: DbType.Int64, value: saveUserId, direction: ParameterDirection.Input);

                baseService.GetList<FunctionModel>("sp_Permission_SaveUserPermission", param: param);
                response.ActionResult = ActionResultData.Success;
            }
            catch (Exception ex)
            {
                response.ActionResult = ActionResultData.Failed;
                response.ErrorData = new ErrorDataModel(ex);
            }
            return response;
        }
    }
}
