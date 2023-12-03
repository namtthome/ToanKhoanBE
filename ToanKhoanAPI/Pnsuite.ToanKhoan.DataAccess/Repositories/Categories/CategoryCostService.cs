using Dapper;
using Microsoft.Extensions.Options;
using System;
using System.Data;
using System.Threading.Tasks;
using vn.com.pnsuite.common.dataaccess.interfaces;
using vn.com.pnsuite.common.models;
using vn.com.pnsuite.toankhoan.dataaccess.Interfaces.Categories;
using vn.com.pnsuite.toankhoan.models.Categories.Costs;

namespace vn.com.pnsuite.toankhoan.dataaccess.Repositories.Categories
{
    public class CategoryCostService : ICategoryCostService
    {

        private readonly IBaseService baseService;
        private readonly AppSettings appSettings;
        public CategoryCostService(IOptions<AppSettings> appSettings, IBaseService baseService)
        {
            this.appSettings = appSettings.Value;
            this.baseService = baseService;
        }
        public async Task<ResponseData> GetAllBySearchAsync(long companyId, long userId, string search)
        {
            ResponseData response = new ResponseData();
            try
            {
                var param = new DynamicParameters();
                param.Add("@CompanyId", dbType: DbType.Int64, value: companyId, direction: ParameterDirection.Input);
                param.Add("@UserId", dbType: DbType.Int64, value: userId, direction: ParameterDirection.Input);
                param.Add("@Json", dbType: DbType.String, value: search, direction: ParameterDirection.Input);
                response.ActionResult = ActionResultData.Success;
                response.ActionData = await baseService.GetListAsync<dynamic>("sp_Category_Cost_GetList", param: param);
            }
            catch (Exception ex)
            {
                response.ActionResult = ActionResultData.Failed;
                response.ErrorData = new ErrorDataModel(ex);
            }
            return response;
        }
        public async Task<ResponseData> GetDetail(long companyId, long userId, string search)
        {
            ResponseData response = new ResponseData();
            try
            {
                var param = new DynamicParameters();
                param.Add("@CompanyId", dbType: DbType.Int64, value: companyId, direction: ParameterDirection.Input);
                param.Add("@UserId", dbType: DbType.Int64, value: userId, direction: ParameterDirection.Input);
                param.Add("@Json", dbType: DbType.String, value: search, direction: ParameterDirection.Input);
                response.ActionData = await baseService.GetSingleAsync<dynamic>("sp_Category_Cost_GetByDetail", param);
                response.ActionResult = ActionResultData.Success;
            }
            catch (Exception ex)
            {
                response.ActionResult = ActionResultData.Failed;
                response.ErrorData = new ErrorDataModel(ex);
            }

            return response;
        }
        public async Task<ResponseData> CreateAsync(long companyId, long userId, string json)
        {
            ResponseData response = new ResponseData();
            try
            {
                var param = new DynamicParameters();
                param.Add("@CompanyId", dbType: DbType.Int64, value: companyId, direction: ParameterDirection.Input);
                param.Add("@UserId", dbType: DbType.Int64, value: userId, direction: ParameterDirection.Input);
                param.Add("@JsonData", dbType: DbType.String, value: json, direction: ParameterDirection.Input);
                await baseService.ExecuteAsync("sp_Category_Cost_Create", param);
                response.ActionResult = ActionResultData.Success;
            }
            catch (Exception ex)
            {
                response.ActionResult = ActionResultData.Failed;
                response.ErrorData = new ErrorDataModel(ex);
            }

            return response;
        }
        public async Task<ResponseData> UpdateAsync(long companyId, long userId, string json)
        {
            ResponseData response = new ResponseData();
            try
            {
                var param = new DynamicParameters();
                param.Add("@CompanyId", dbType: DbType.Int64, value: companyId, direction: ParameterDirection.Input);
                param.Add("@UserId", dbType: DbType.Int64, value: userId, direction: ParameterDirection.Input);
                param.Add("@Json", dbType: DbType.String, value: json, direction: ParameterDirection.Input);
                await baseService.ExecuteAsync("sp_Category_Cost_Update", param);
                response.ActionResult = ActionResultData.Success;
            }
            catch (Exception ex)
            {
                response.ActionResult = ActionResultData.Failed;
                response.ErrorData = new ErrorDataModel(ex);
            }

            return response;
        }
        public async Task<ResponseData> Delete(long companyId, long userId, string json)
        {
            ResponseData response = new ResponseData();
            try
            {
                var param = new DynamicParameters();
                param.Add("@CompanyId", dbType: DbType.Int64, value: companyId, direction: ParameterDirection.Input);
                param.Add("@UserId", dbType: DbType.Int64, value: userId, direction: ParameterDirection.Input);
                param.Add("@Json", dbType: DbType.String, value: json, direction: ParameterDirection.Input);
                await baseService.ExecuteAsync("sp_Category_Cost_Delete", param);
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
