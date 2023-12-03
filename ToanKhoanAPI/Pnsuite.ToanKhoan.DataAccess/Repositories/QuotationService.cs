using Dapper;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vn.com.pnsuite.common.dataaccess.interfaces;
using vn.com.pnsuite.common.models;
using vn.com.pnsuite.toankhoan.dataaccess.Interfaces;

namespace vn.com.pnsuite.toankhoan.dataaccess.Repositories
{
    public class QuotationService : IQuotationService
    {
        private readonly IBaseService baseService;
        private readonly AppSettings appSettings;
        public QuotationService(IOptions<AppSettings> appSettings, IBaseService baseService)
        {
            this.appSettings = appSettings.Value;
            this.baseService = baseService;
        }
        public async Task<ResponseData> CreateQuotationAsync(long companyId, long userId, string json)
        {
            ResponseData response = new ResponseData();
            try
            {
                var param = new DynamicParameters();
                param.Add("@CompanyId", dbType: DbType.Int64, value: companyId, direction: ParameterDirection.Input);
                param.Add("@UserId", dbType: DbType.Int64, value: userId, direction: ParameterDirection.Input);
                param.Add("@Json", dbType: DbType.String, value: json, direction: ParameterDirection.Input);
                param.Add("@Code", dbType: DbType.String, value: "", direction: ParameterDirection.Output);

                await baseService.ExecuteAsync("dbo.sp_QuotationPrice_Insert", param);
                response.ActionResult = ActionResultData.Success;
                response.ActionData = new { code = param.Get<String>("@Code") };
            }
            catch (Exception ex)
            {
                response.ActionResult = ActionResultData.Failed;
                response.ErrorData = new ErrorDataModel(ex);
            }
            return response;
        }
        public async Task<ResponseData> DeleteQuotationAsync(long companyId, long userId, string json)
        {
            ResponseData response = new ResponseData();
            try
            {
                var param = new DynamicParameters();
                param.Add("@CompanyId", dbType: DbType.Int64, value: companyId, direction: ParameterDirection.Input);
                param.Add("@UserId", dbType: DbType.Int64, value: userId, direction: ParameterDirection.Input);
                param.Add("@Json", dbType: DbType.String, value: json, direction: ParameterDirection.Input);

                await baseService.ExecuteAsync("dbo.sp_QuotationPrice_Delete", param);
                response.ActionResult = ActionResultData.Success;
            }
            catch (Exception ex)
            {
                response.ActionResult = ActionResultData.Failed;
                response.ErrorData = new ErrorDataModel(ex);
            }

            return response;
        }
        public async Task<ResponseData> GetAllQuotationBySearchAsync(long userId, long companyId, string search)
        {
            ResponseData response = new ResponseData();
            try
            {
                var param = new DynamicParameters();
                param.Add("@CompanyId", dbType: DbType.Int64, value: companyId, direction: ParameterDirection.Input);
                param.Add("@UserId", dbType: DbType.Int64, value: userId, direction: ParameterDirection.Input);
                param.Add("@Json", dbType: DbType.String, value: search, direction: ParameterDirection.Input);
                response.ActionData = await baseService.GetListAsync<dynamic>("dbo.sp_QuotationPrice_GetList", param);
                response.ActionResult = ActionResultData.Success;
            }
            catch (Exception ex)
            {
                response.ActionResult = ActionResultData.Failed;
                response.ErrorData = new ErrorDataModel(ex);
            }
            return response;
        }
        public async Task<ResponseData> GetQuotationDetail(long companyId, long userId, string json)
        {
            ResponseData response = new ResponseData();
            try
            {
                var param = new DynamicParameters();
                param.Add("@CompanyId", dbType: DbType.Int64, value: companyId, direction: ParameterDirection.Input);
                param.Add("@UserId", dbType: DbType.Int64, value: userId, direction: ParameterDirection.Input);
                param.Add("@Json", dbType: DbType.String, value: json, direction: ParameterDirection.Input);

                using IDbConnection db = baseService.Connection();
                var result = await db.QueryMultipleAsync("dbo.sp_QuotationPrice_GetByDetail", param, commandType: CommandType.StoredProcedure);
                var quotation = result.Read<dynamic>().FirstOrDefault();
                var quotationDetails = result.Read<dynamic>().AsList();
                if (quotation != null)
                {
                    quotation.details = quotationDetails;
                    response.ActionData = quotation;
                    response.ActionResult = ActionResultData.Success;
                }
                else
                {
                    throw new Exception("Lỗi không xác định, dữ liệu trống");
                }

            }
            catch (Exception ex)
            {
                response.ActionResult = ActionResultData.Failed;
                response.ErrorData = new ErrorDataModel(ex);
            }

            return response;
        }
        public async Task<ResponseData> GetCopyQuotation(long companyId, long userId, string json)
        {
            ResponseData response = new ResponseData();
            try
            {
                var param = new DynamicParameters();
                param.Add("@CompanyId", dbType: DbType.Int64, value: companyId, direction: ParameterDirection.Input);
                param.Add("@UserId", dbType: DbType.Int64, value: userId, direction: ParameterDirection.Input);
                param.Add("@Json", dbType: DbType.String, value: json, direction: ParameterDirection.Input);

                using IDbConnection db = baseService.Connection();
                var result = await db.QueryMultipleAsync("dbo.sp_QuotationPrice_GetCopyDataById", param, commandType: CommandType.StoredProcedure);
                var quotation = result.Read<dynamic>().FirstOrDefault();
                var quotationDetails = result.Read<dynamic>().AsList();
                if (quotation != null)
                {
                    quotation.details = quotationDetails;
                    response.ActionData = quotation;
                    response.ActionResult = ActionResultData.Success;
                }
                else
                {
                    throw new Exception("Lỗi không xác định, dữ liệu trống");
                }

            }
            catch (Exception ex)
            {
                response.ActionResult = ActionResultData.Failed;
                response.ErrorData = new ErrorDataModel(ex);
            }

            return response;
        }
        public async Task<ResponseData> UpdateQuotationAsync(long companyId, long userId, string json)
        {
            ResponseData response = new ResponseData();
            try
            {
                var param = new DynamicParameters();
                param.Add("@CompanyId", dbType: DbType.Int64, value: companyId, direction: ParameterDirection.Input);
                param.Add("@UserId", dbType: DbType.Int64, value: userId, direction: ParameterDirection.Input);
                param.Add("@Json", dbType: DbType.String, value: json, direction: ParameterDirection.Input);

                await baseService.ExecuteAsync("dbo.sp_QuotationPrice_Update", param);
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
