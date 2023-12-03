using Dapper;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using vn.com.pnsuite.common.dataaccess.interfaces;
using vn.com.pnsuite.common.models;
using vn.com.pnsuite.toankhoan.dataaccess.interfaces;
using vn.com.pnsuite.toankhoan.models.common;

namespace vn.com.pnsuite.toankhoan.dataaccess.repositories
{
    public class CommonService : ICommonService
    {
        private readonly IBaseService baseService;
        private readonly AppSettings appSettings;
        public CommonService(IOptions<AppSettings> appSettings, IBaseService baseService)
        {
            this.appSettings = appSettings.Value;
            this.baseService = baseService;
        }
        public ResponseData create(string jsonData, long userId)
        {
            ResponseData response = new ResponseData();
            try
            {
                var param = new DynamicParameters();
                param.Add("@JsonData ", dbType: DbType.String, value: jsonData, direction: ParameterDirection.Input);
                param.Add("@UserId", dbType: DbType.Int64, value: userId, direction: ParameterDirection.Input);
                baseService.Update("sp_Common_InsertOrUpdate", param);

                response.ActionResult = ActionResultData.Success;
            }
            catch (Exception ex)
            {
                response.ActionResult = ActionResultData.Failed;
                response.ErrorData = new ErrorDataModel(ex) ;
            }

            return response;
        }
        public ResponseData delete(int id, long userId)
        {
            ResponseData response = new ResponseData();
            try
            {
                baseService.Delete("sp_Common_Delete", id, userId);
                response.ActionResult = ActionResultData.Success;
            }
            catch (Exception ex)
            {
                response.ActionResult = ActionResultData.Failed;
                response.ErrorData = new ErrorDataModel(ex) ;
            }
            return response;
        }
        public ResponseData getAllByTaxcode(CommonRequest request)
        {
            ResponseData response = new ResponseData();
            try
            {
                var param = new DynamicParameters();
                param.Add("@Taxcode", dbType: DbType.String, value: request.Taxcode, direction: ParameterDirection.Input);
                param.Add("@Type ", dbType: DbType.String, value: request.GetRequestValue("Type") == null ? "" : request.GetRequestValue("Type").Value.ToString(), direction: ParameterDirection.Input);
                param.Add("@SearchValue ", dbType: DbType.String, value: request.GetRequestValue("Search") == null ?  null : request.GetRequestValue("Search").Value.ToString().ToString(), direction: ParameterDirection.Input);
                response.ActionResult = ActionResultData.Success;
                response.ActionData = baseService.GetList<Object>("sp_Common_GetListByTaxcode", param: param);
            }
            catch (Exception ex)
            {
                response.ActionResult = ActionResultData.Failed;
                response.ErrorData = new ErrorDataModel(ex) ;
            }
            return response;
        }
        public ResponseData getById(long id)
        {
            ResponseData response = new ResponseData();
            try
            {
                response.ActionResult = ActionResultData.Success;
                response.ActionData = baseService.GetById<Object>("sp_Common_GetById", id);
            }
            catch (Exception ex)
            {
                response.ActionResult = ActionResultData.Failed;
                response.ErrorData = new ErrorDataModel(ex) ;
            }
            return response;
        }
        public ResponseData getTypeByCommonId(int id)
        {
            ResponseData response = new ResponseData();
            try
            {
                var param = new DynamicParameters();
                param.Add("@CommonId", dbType: DbType.Int32, value: id, direction: ParameterDirection.Input);
                response.ActionResult = ActionResultData.Success;
                response.ActionData = baseService.GetSingle<CommonType>("sp_CommonType_GetByCommonId", param: param);
            }
            catch (Exception ex)
            {
                response.ActionResult = ActionResultData.Failed;
                response.ErrorData = new ErrorDataModel(ex);
            }
            return response;
        }
        public ResponseData getTypeByCode(String code)
        {
            ResponseData response = new ResponseData();
            try
            {
                var param = new DynamicParameters();
                param.Add("@Code", dbType: DbType.String, value: code, direction: ParameterDirection.Input);
                response.ActionResult = ActionResultData.Success;
                response.ActionData = baseService.GetSingle<CommonType>("sp_CommonType_GetByCode", param: param);
            }
            catch (Exception ex)
            {
                response.ActionResult = ActionResultData.Failed;
                response.ErrorData = new ErrorDataModel(ex);
            }
            return response;
        }
        public ResponseData getClientFileNeedUpdate(int companyId, long userId, string json)
        {
            ResponseData response = new ResponseData();
            try
            {
                var param = new DynamicParameters();
                param.Add("@CompanyId", dbType: DbType.Int32, value: companyId, direction: ParameterDirection.Input);
                param.Add("@UserId", dbType: DbType.Int64, value: userId, direction: ParameterDirection.Input);
                param.Add("@Json", dbType: DbType.String, value: json, direction: ParameterDirection.Input);

                response.ActionResult = ActionResultData.Success;
                response.ActionData = baseService.GetList<dynamic>("dbo.sp_ClientFile_List", param);
            }
            catch (Exception ex)
            {
                response.ActionResult = ActionResultData.Failed;
                response.ErrorData = new ErrorDataModel(ex);
            }
            return response;
        }
        public ResponseData removeClientFileNeedUpdate(int companyId, long userId, string json)
        {
            ResponseData response = new ResponseData();
            try
            {
                var param = new DynamicParameters();
                param.Add("@CompanyId", dbType: DbType.Int64, value: companyId, direction: ParameterDirection.Input);
                param.Add("@UserId", dbType: DbType.Int64, value: userId, direction: ParameterDirection.Input);
                param.Add("@Json", dbType: DbType.String, value: json, direction: ParameterDirection.Input);

                response.ActionResult = ActionResultData.Success;
                baseService.Update("dbo.sp_ClientFile_RemoveNeedUpdate", param);
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
