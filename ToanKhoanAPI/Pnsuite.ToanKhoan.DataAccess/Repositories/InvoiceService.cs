using Dapper;
using Microsoft.Extensions.Options;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using vn.com.pnsuite.common.dataaccess.interfaces;
using vn.com.pnsuite.common.models;
using vn.com.pnsuite.toankhoan.dataaccess.Interfaces;

namespace vn.com.pnsuite.toankhoan.dataaccess.Repositories
{
    public class InvoiceService : IInvoiceService
    {

        private readonly IBaseService baseService;
        private readonly AppSettings appSettings;
        public InvoiceService(IOptions<AppSettings> appSettings, IBaseService baseService)
        {
            this.appSettings = appSettings.Value;
            this.baseService = baseService;
        }


        #region Invoice
        public async Task<ResponseData> CreateInvoiceAsync(long companyId, long userId, string json)
        {
            ResponseData response = new ResponseData();
            try
            {
                var param = new DynamicParameters();
                param.Add("@CompanyId", dbType: DbType.Int64, value: companyId, direction: ParameterDirection.Input);
                param.Add("@UserId", dbType: DbType.Int64, value: userId, direction: ParameterDirection.Input);
                param.Add("@Json", dbType: DbType.String, value: json, direction: ParameterDirection.Input);
                param.Add("@Code", dbType: DbType.String, value: "", direction: ParameterDirection.Output);

                await baseService.ExecuteAsync("sp_Invoice_Insert", param);
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
        public async Task<ResponseData> DeleteInvoiceAsync(long companyId, long userId, string json)
        {
            ResponseData response = new ResponseData();
            try
            {
                var param = new DynamicParameters();
                param.Add("@CompanyId", dbType: DbType.Int64, value: companyId, direction: ParameterDirection.Input);
                param.Add("@UserId", dbType: DbType.Int64, value: userId, direction: ParameterDirection.Input);
                param.Add("@Json", dbType: DbType.String, value: json, direction: ParameterDirection.Input);

                await baseService.ExecuteAsync("dbo.sp_Invoice_Delete", param);
                response.ActionResult = ActionResultData.Success;
            }
            catch (Exception ex)
            {
                response.ActionResult = ActionResultData.Failed;
                response.ErrorData = new ErrorDataModel(ex);
            }

            return response;
        }
        public async Task<ResponseData> GetAllInvoiceBySearchAsync(long userId, long companyId, string search)
        {
            ResponseData response = new ResponseData();
            try
            {
                var param = new DynamicParameters();
                param.Add("@CompanyId", dbType: DbType.Int64, value: companyId, direction: ParameterDirection.Input);
                param.Add("@UserId", dbType: DbType.Int64, value: userId, direction: ParameterDirection.Input);
                param.Add("@Json", dbType: DbType.String, value: search, direction: ParameterDirection.Input);
                response.ActionData = await baseService.GetListAsync<dynamic>("dbo.sp_Invoice_GetList", param);
                response.ActionResult = ActionResultData.Success;
            }
            catch (Exception ex)
            {
                response.ActionResult = ActionResultData.Failed;
                response.ErrorData = new ErrorDataModel(ex);
            }
            return response;
        }
        public async Task<ResponseData> GetDetailInvoice(long companyId, long userId, string json)
        {
            ResponseData response = new ResponseData();
            try
            {
                var param = new DynamicParameters();
                param.Add("@CompanyId", dbType: DbType.Int64, value: companyId, direction: ParameterDirection.Input);
                param.Add("@UserId", dbType: DbType.Int64, value: userId, direction: ParameterDirection.Input);
                param.Add("@Json", dbType: DbType.String, value: json, direction: ParameterDirection.Input);

                using IDbConnection db = baseService.Connection();
                var result = await db.QueryMultipleAsync("sp_Invoice_GetByDetail", param, commandType: CommandType.StoredProcedure);
                var invoice = result.Read<dynamic>().FirstOrDefault();
                var invoiceDetails = result.Read<dynamic>().AsList();
                var invoiceDetailProductExtras = result.Read<dynamic>().AsList();
                if (invoice != null)
                {
                    invoice.details = invoiceDetails;
                    foreach (var detail in invoiceDetails)
                    {
                        detail.extraInfo = invoiceDetailProductExtras
                            .Where(item => item.InvoiceDetailId == detail.Id)
                            .ToList();
                    }
                    response.ActionData = invoice;
                    response.ActionResult = ActionResultData.Success;
                } else
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
        public async Task<ResponseData> UpdateInvoiceAsync(long companyId, long userId, string json)
        {
            ResponseData response = new ResponseData();
            try
            {
                var param = new DynamicParameters();
                param.Add("@CompanyId", dbType: DbType.Int64, value: companyId, direction: ParameterDirection.Input);
                param.Add("@UserId", dbType: DbType.Int64, value: userId, direction: ParameterDirection.Input);
                param.Add("@Json", dbType: DbType.String, value: json, direction: ParameterDirection.Input);

                await baseService.ExecuteAsync("sp_Invoice_Update", param);
                response.ActionResult = ActionResultData.Success;
            }
            catch (Exception ex)
            {
                response.ActionResult = ActionResultData.Failed;
                response.ErrorData = new ErrorDataModel(ex);
            }
            return response;
        }
        public async Task<ResponseData> GetCopyInvoice(long companyId, long userId, string json)
        {
            ResponseData response = new ResponseData();
            try
            {
                var param = new DynamicParameters();
                param.Add("@CompanyId", dbType: DbType.Int64, value: companyId, direction: ParameterDirection.Input);
                param.Add("@UserId", dbType: DbType.Int64, value: userId, direction: ParameterDirection.Input);
                param.Add("@Json", dbType: DbType.String, value: json, direction: ParameterDirection.Input);

                using IDbConnection db = baseService.Connection();
                var result = await db.QueryMultipleAsync("dbo.sp_Invoice_CopyById", param, commandType: CommandType.StoredProcedure);
                var invoice = result.Read<dynamic>().FirstOrDefault();
                var invoiceDetails = result.Read<dynamic>().AsList();
                
                if (invoice != null)
                {
                    invoice.details = invoiceDetails;
                    response.ActionData = invoice;
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
        public async Task<ResponseData> GetNewInvoiceFromQuotation(long companyId, long userId, string json)
        {
            ResponseData response = new ResponseData();
            try
            {
                var param = new DynamicParameters();
                param.Add("@CompanyId", dbType: DbType.Int64, value: companyId, direction: ParameterDirection.Input);
                param.Add("@UserId", dbType: DbType.Int64, value: userId, direction: ParameterDirection.Input);
                param.Add("@Json", dbType: DbType.String, value: json, direction: ParameterDirection.Input);

                using IDbConnection db = baseService.Connection();
                var result = await db.QueryMultipleAsync("dbo.sp_Invoice_CreateByQuotation", param, commandType: CommandType.StoredProcedure);
                var invoice = result.Read<dynamic>().FirstOrDefault();
                var invoiceDetails = result.Read<dynamic>().AsList();

                if (invoice != null)
                {
                    invoice.details = invoiceDetails;
                    response.ActionData = invoice;
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
        #endregion
        #region Return

        public async Task<ResponseData> GetReturnDetailAsync(long companyId, long userId, string json)
        {
            ResponseData response = new ResponseData();
            try
            {
                var param = new DynamicParameters();
                param.Add("@CompanyId", dbType: DbType.Int64, value: companyId, direction: ParameterDirection.Input);
                param.Add("@UserId", dbType: DbType.Int64, value: userId, direction: ParameterDirection.Input);
                param.Add("@Json", dbType: DbType.String, value: json, direction: ParameterDirection.Input);

                using IDbConnection db = baseService.Connection();
                var result = await db.QueryMultipleAsync("sp_InvoiceReturn_GetByDetail", param, commandType: CommandType.StoredProcedure);
                var invoiceReturn = result.Read<dynamic>().FirstOrDefault();
                var invoiceReturnDetails = result.Read<dynamic>().AsList();
                var invoiceReturnDetailProductExtras = result.Read<dynamic>().AsList();
                if (invoiceReturn != null)
                {
                    invoiceReturn.details = invoiceReturnDetails;
                    foreach (var detail in invoiceReturnDetails)
                    {
                        detail.extraInfo = invoiceReturnDetailProductExtras
                            .Where(item => item.InvoiceDetailId == detail.Id)
                            .ToList();
                    }
                    response.ActionData = invoiceReturn;
                    response.ActionResult = ActionResultData.Success;
                } else
                {
                    response.ErrorData = new ErrorDataModel("SQLERROR", "Không thấy dữ liệu");
                    response.ActionResult = ActionResultData.Failed;
                }
                
            }
            catch (Exception ex)
            {
                response.ActionResult = ActionResultData.Failed;
                response.ErrorData = new ErrorDataModel(ex);
            }

            return response;
        }
        public async Task<ResponseData> GetCopyDataReturnDetailAsync(long companyId, long userId, string json)
        {
            ResponseData response = new ResponseData();
            try
            {
                var param = new DynamicParameters();
                param.Add("@CompanyId", dbType: DbType.Int64, value: companyId, direction: ParameterDirection.Input);
                param.Add("@UserId", dbType: DbType.Int64, value: userId, direction: ParameterDirection.Input);
                param.Add("@Json", dbType: DbType.String, value: json, direction: ParameterDirection.Input);

                using IDbConnection db = baseService.Connection();
                var result = await db.QueryMultipleAsync("sp_InvoiceReturn_GetCopyDataByDetail", param, commandType: CommandType.StoredProcedure);
                var invoiceReturn = result.Read<dynamic>().FirstOrDefault();
                var invoiceReturnDetails = result.Read<dynamic>().AsList();
                if (invoiceReturn != null)
                {
                    invoiceReturn.details = invoiceReturnDetails;
                    
                    response.ActionData = invoiceReturn;
                    response.ActionResult = ActionResultData.Success;
                }
                else
                {
                    response.ErrorData = new ErrorDataModel("SQLERROR", "Không thấy dữ liệu");
                    response.ActionResult = ActionResultData.Failed;
                }

            }
            catch (Exception ex)
            {
                response.ActionResult = ActionResultData.Failed;
                response.ErrorData = new ErrorDataModel(ex);
            }

            return response;
        }
        public async Task<ResponseData> DeleteReturnAsync(long companyId, long userId, string json)
        {
            ResponseData response = new ResponseData();
            try
            {
                var param = new DynamicParameters();
                param.Add("@CompanyId", dbType: DbType.Int64, value: companyId, direction: ParameterDirection.Input);
                param.Add("@UserId", dbType: DbType.Int64, value: userId, direction: ParameterDirection.Input);
                param.Add("@Json", dbType: DbType.String, value: json, direction: ParameterDirection.Input);

                await baseService.ExecuteAsync("dbo.sp_InvoiceReturn_Delete", param);
                response.ActionResult = ActionResultData.Success;
            }
            catch (Exception ex)
            {
                response.ActionResult = ActionResultData.Failed;
                response.ErrorData = new ErrorDataModel(ex);
            }

            return response;
        }

        public async Task<ResponseData> CreateReturnAsync(long companyId, long userId, string json)
        {
            ResponseData response = new ResponseData();
            try
            {
                var param = new DynamicParameters();
                param.Add("@CompanyId", dbType: DbType.Int64, value: companyId, direction: ParameterDirection.Input);
                param.Add("@UserId", dbType: DbType.Int64, value: userId, direction: ParameterDirection.Input);
                param.Add("@Json", dbType: DbType.String, value: json, direction: ParameterDirection.Input);
                param.Add("@Code", dbType: DbType.String, value: "", direction: ParameterDirection.Output);

                await baseService.ExecuteAsync("sp_InvoiceReturn_Insert", param);
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

        public async Task<ResponseData> UpdateReturnAsync(long companyId, long userId, string json)
        {
            ResponseData response = new ResponseData();
            try
            {
                var param = new DynamicParameters();
                param.Add("@CompanyId", dbType: DbType.Int64, value: companyId, direction: ParameterDirection.Input);
                param.Add("@UserId", dbType: DbType.Int64, value: userId, direction: ParameterDirection.Input);
                param.Add("@Json", dbType: DbType.String, value: json, direction: ParameterDirection.Input);

                await baseService.ExecuteAsync("sp_InvoiceReturn_Update", param);
                response.ActionResult = ActionResultData.Success;
            }
            catch (Exception ex)
            {
                response.ActionResult = ActionResultData.Failed;
                response.ErrorData = new ErrorDataModel(ex);
            }
            return response;
        }

        public async Task<ResponseData> GetAllReturnBySearchAsync(long userId, long companyId, string search)
        {
            ResponseData response = new ResponseData();
            try
            {
                var param = new DynamicParameters();
                param.Add("@CompanyId", dbType: DbType.Int64, value: companyId, direction: ParameterDirection.Input);
                param.Add("@UserId", dbType: DbType.Int64, value: userId, direction: ParameterDirection.Input);
                param.Add("@Json", dbType: DbType.String, value: search, direction: ParameterDirection.Input);
                response.ActionData = await baseService.GetListAsync<dynamic>("dbo.sp_InvoiceReturn_GetList", param);
                response.ActionResult = ActionResultData.Success;
            }
            catch (Exception ex)
            {
                response.ActionResult = ActionResultData.Failed;
                response.ErrorData = new ErrorDataModel(ex);
            }
            return response;
        }

        #endregion
    }
}
