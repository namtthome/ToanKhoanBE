using Dapper;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using vn.com.pnsuite.common.dataaccess.interfaces;
using vn.com.pnsuite.common.models;
using vn.com.pnsuite.toankhoan.dataaccess.Interfaces;

namespace vn.com.pnsuite.toankhoan.dataaccess.Repositories
{
    public class ReportService : IReportService
    {
        private readonly IBaseService baseService;
        private readonly AppSettings appSettings;
        public ReportService(IOptions<AppSettings> appSettings, IBaseService baseService)
        {
            this.appSettings = appSettings.Value;
            this.baseService = baseService;
        }
        public ResponseData GetInvoiceReportById(long companyId, long userId, string json)
        {
            ResponseData response = new ResponseData();
            try
            {
                var param = new DynamicParameters();
                param.Add("@CompanyId", dbType: DbType.Int64, value: companyId, direction: ParameterDirection.Input);
                param.Add("@UserId", dbType: DbType.Int64, value: userId, direction: ParameterDirection.Input);
                param.Add("@Json", dbType: DbType.String, value: json, direction: ParameterDirection.Input);

                response.ActionResult = ActionResultData.Success;
                var result = baseService.Connection().QueryMultiple("dbo.sp_Report_Invoice", param: param, commandType: CommandType.StoredProcedure);
                var header = result.ReadFirst<dynamic>();
                var details = result.Read<dynamic>();

                if (header != null)
                {
                    header.details = details;
                }

                response.ActionResult = ActionResultData.Success;
                response.ActionData = header;
            }
            catch (Exception ex)
            {
                response.ActionResult = ActionResultData.Failed;
                response.ErrorData = new ErrorDataModel(ex);
            }
            return response;
        }
        public ResponseData GetDailyReportP1ById(long companyId, long userId, string json)
        {
            ResponseData response = new ResponseData();
            try
            {
                var param = new DynamicParameters();
                param.Add("@CompanyId", dbType: DbType.Int64, value: companyId, direction: ParameterDirection.Input);
                param.Add("@UserId", dbType: DbType.Int64, value: userId, direction: ParameterDirection.Input);
                param.Add("@Json", dbType: DbType.String, value: json, direction: ParameterDirection.Input);

                response.ActionResult = ActionResultData.Success;

                var result = baseService.Connection().QueryMultiple("dbo.sp_Report_Daily_P1", param: param, commandType: CommandType.StoredProcedure);
                var balance = result.ReadFirst<dynamic>();
                var invoices = result.Read<dynamic>();

                response.ActionResult = ActionResultData.Success;
                response.ActionData = new { cashBalance = balance.cashBalance, invoices };
            }
            catch (Exception ex)
            {
                response.ActionResult = ActionResultData.Failed;
                response.ErrorData = new ErrorDataModel(ex);
            }
            return response;
        }
        public ResponseData GetDailyReportP2ById(long companyId, long userId, string json)
        {
            ResponseData response = new ResponseData();
            try
            {
                var param = new DynamicParameters();
                param.Add("@CompanyId", dbType: DbType.Int64, value: companyId, direction: ParameterDirection.Input);
                param.Add("@UserId", dbType: DbType.Int64, value: userId, direction: ParameterDirection.Input);
                param.Add("@Json", dbType: DbType.String, value: json, direction: ParameterDirection.Input);

                response.ActionResult = ActionResultData.Success;
                var result = baseService.Connection().QueryMultiple("dbo.sp_Report_Daily_P2", param: param, commandType: CommandType.StoredProcedure);
                var purchase = result.Read<dynamic>();
                //var debt = result.Read<dynamic>();

                var data = new { purchase};
                response.ActionResult = ActionResultData.Success;
                response.ActionData = data;
            }
            catch (Exception ex)
            {
                response.ActionResult = ActionResultData.Failed;
                response.ErrorData = new ErrorDataModel(ex);
            }
            return response;
        }
        public ResponseData GetDailyReportP3ById(long companyId, long userId, string json)
        {
            ResponseData response = new ResponseData();
            try
            {
                var param = new DynamicParameters();
                param.Add("@CompanyId", dbType: DbType.Int64, value: companyId, direction: ParameterDirection.Input);
                param.Add("@UserId", dbType: DbType.Int64, value: userId, direction: ParameterDirection.Input);
                param.Add("@Json", dbType: DbType.String, value: json, direction: ParameterDirection.Input);

                response.ActionResult = ActionResultData.Success;
                var result = baseService.Connection().QueryMultiple("dbo.sp_Report_Daily_P3", param: param, commandType: CommandType.StoredProcedure);
                var receive = result.Read<dynamic>();
                var payment = result.Read<dynamic>();
                var debt = result.Read<dynamic>();

                var data = new { receive, payment, debt};
                response.ActionResult = ActionResultData.Success;
                response.ActionData = data;
            }
            catch (Exception ex)
            {
                response.ActionResult = ActionResultData.Failed;
                response.ErrorData = new ErrorDataModel(ex);
            }
            return response;
        }
        public ResponseData GetDailyReportListPurchase(long companyId, long userId, string json)
        {
            ResponseData response = new ResponseData();
            try
            {
                var param = new DynamicParameters();
                param.Add("@CompanyId", dbType: DbType.Int64, value: companyId, direction: ParameterDirection.Input);
                param.Add("@UserId", dbType: DbType.Int64, value: userId, direction: ParameterDirection.Input);
                param.Add("@Json", dbType: DbType.String, value: json, direction: ParameterDirection.Input);

                response.ActionResult = ActionResultData.Success;
                var result = baseService.GetList<dynamic>("dbo.sp_Report_Daily_ListPurchase", param: param);

                response.ActionResult = ActionResultData.Success;
                response.ActionData = result;
            }
            catch (Exception ex)
            {
                response.ActionResult = ActionResultData.Failed;
                response.ErrorData = new ErrorDataModel(ex);
            }
            return response;
        }
        public ResponseData GetDailyReportListInvoice(long companyId, long userId, string json)
        {
            ResponseData response = new ResponseData();
            try
            {
                var param = new DynamicParameters();
                param.Add("@CompanyId", dbType: DbType.Int64, value: companyId, direction: ParameterDirection.Input);
                param.Add("@UserId", dbType: DbType.Int64, value: userId, direction: ParameterDirection.Input);
                param.Add("@Json", dbType: DbType.String, value: json, direction: ParameterDirection.Input);

                response.ActionResult = ActionResultData.Success;
                var result = baseService.GetList<dynamic>("dbo.sp_Report_Daily_ListInvoice", param: param);

                response.ActionResult = ActionResultData.Success;
                response.ActionData = result;
            }
            catch (Exception ex)
            {
                response.ActionResult = ActionResultData.Failed;
                response.ErrorData = new ErrorDataModel(ex);
            }
            return response;
        }
        public ResponseData GetInventoryReportBalance(long companyId, long userId, string json)
        {
            ResponseData response = new ResponseData();
            try
            {
                var param = new DynamicParameters();
                param.Add("@CompanyId", dbType: DbType.Int64, value: companyId, direction: ParameterDirection.Input);
                param.Add("@UserId", dbType: DbType.Int64, value: userId, direction: ParameterDirection.Input);
                param.Add("@Json", dbType: DbType.String, value: json, direction: ParameterDirection.Input);

                response.ActionResult = ActionResultData.Success;
                var result = baseService.GetList<dynamic>("dbo.sp_Report_InventoryBalance", param: param);

                response.ActionResult = ActionResultData.Success;
                response.ActionData = result;
            }
            catch (Exception ex)
            {
                response.ActionResult = ActionResultData.Failed;
                response.ErrorData = new ErrorDataModel(ex);
            }
            return response;
        }
        public ResponseData GetPayableReport(long companyId, long userId, string json)
        {
            ResponseData response = new ResponseData();
            try
            {
                var param = new DynamicParameters();
                param.Add("@CompanyId", dbType: DbType.Int64, value: companyId, direction: ParameterDirection.Input);
                param.Add("@UserId", dbType: DbType.Int64, value: userId, direction: ParameterDirection.Input);
                param.Add("@Json", dbType: DbType.String, value: json, direction: ParameterDirection.Input);

                response.ActionResult = ActionResultData.Success;
                var result = baseService.GetList<dynamic>("dbo.sp_Report_Payables", param: param);

                response.ActionResult = ActionResultData.Success;
                response.ActionData = result;
            }
            catch (Exception ex)
            {
                response.ActionResult = ActionResultData.Failed;
                response.ErrorData = new ErrorDataModel(ex);
            }
            return response;
        }
        public ResponseData GetReceivableReport(long companyId, long userId, string json)
        {
            ResponseData response = new ResponseData();
            try
            {
                var param = new DynamicParameters();
                param.Add("@CompanyId", dbType: DbType.Int64, value: companyId, direction: ParameterDirection.Input);
                param.Add("@UserId", dbType: DbType.Int64, value: userId, direction: ParameterDirection.Input);
                param.Add("@Json", dbType: DbType.String, value: json, direction: ParameterDirection.Input);

                response.ActionResult = ActionResultData.Success;
                var result = baseService.GetList<dynamic>("dbo.sp_Report_Receivables", param: param);

                response.ActionResult = ActionResultData.Success;
                response.ActionData = result;
            }
            catch (Exception ex)
            {
                response.ActionResult = ActionResultData.Failed;
                response.ErrorData = new ErrorDataModel(ex);
            }
            return response;
        }
        public ResponseData GetBankReport(long companyId, long userId, string json)
        {
            ResponseData response = new ResponseData();
            try
            {
                var param = new DynamicParameters();
                param.Add("@CompanyId", dbType: DbType.Int64, value: companyId, direction: ParameterDirection.Input);
                param.Add("@UserId", dbType: DbType.Int64, value: userId, direction: ParameterDirection.Input);
                param.Add("@Json", dbType: DbType.String, value: json, direction: ParameterDirection.Input);

                var result = baseService.Connection().QueryMultiple("dbo.sp_Report_BankReport", param: param, commandType: CommandType.StoredProcedure);
                
                var info = result.ReadFirst<dynamic>();
                var data = result.Read<dynamic>();

                response.ActionResult = ActionResultData.Success;
                response.ActionData = new { info.companyName, info.bank, info.bankAccount, info.description, info.balance0, info.balance1, details = data };
            }
            catch (Exception ex)
            {
                response.ActionResult = ActionResultData.Failed;
                response.ErrorData = new ErrorDataModel(ex);
            }
            return response;
        }
        public ResponseData GetCashReport(long companyId, long userId, string json)
        {
            ResponseData response = new ResponseData();
            try
            {
                var param = new DynamicParameters();
                param.Add("@CompanyId", dbType: DbType.Int64, value: companyId, direction: ParameterDirection.Input);
                param.Add("@UserId", dbType: DbType.Int64, value: userId, direction: ParameterDirection.Input);
                param.Add("@Json", dbType: DbType.String, value: json, direction: ParameterDirection.Input);

                var result = baseService.Connection().QueryMultiple("dbo.sp_Report_CashReport", param: param, commandType: CommandType.StoredProcedure);
                var info = result.ReadFirst<dynamic>();
                var data = result.Read<dynamic>();

                response.ActionResult = ActionResultData.Success;
                response.ActionData = new { info.companyName, info.balance0, info.balance1, details = data };
            }
            catch (Exception ex)
            {
                response.ActionResult = ActionResultData.Failed;
                response.ErrorData = new ErrorDataModel(ex);
            }
            return response;
        }
        public ResponseData GetReceivableDetailReport(long companyId, long userId, string json)
        {
            ResponseData response = new ResponseData();
            try
            {
                var param = new DynamicParameters();
                param.Add("@CompanyId", dbType: DbType.Int64, value: companyId, direction: ParameterDirection.Input);
                param.Add("@UserId", dbType: DbType.Int64, value: userId, direction: ParameterDirection.Input);
                param.Add("@Json", dbType: DbType.String, value: json, direction: ParameterDirection.Input);

                response.ActionResult = ActionResultData.Success;
                var result = baseService.GetList<dynamic>("dbo.sp_Report_DetailReceipt", param: param);

                response.ActionResult = ActionResultData.Success;
                response.ActionData = result;
            }
            catch (Exception ex)
            {
                response.ActionResult = ActionResultData.Failed;
                response.ErrorData = new ErrorDataModel(ex);
            }
            return response;
        }
        public ResponseData GetPayableDetailReport(long companyId, long userId, string json)
        {
            ResponseData response = new ResponseData();
            try
            {
                var param = new DynamicParameters();
                param.Add("@CompanyId", dbType: DbType.Int64, value: companyId, direction: ParameterDirection.Input);
                param.Add("@UserId", dbType: DbType.Int64, value: userId, direction: ParameterDirection.Input);
                param.Add("@Json", dbType: DbType.String, value: json, direction: ParameterDirection.Input);

                response.ActionResult = ActionResultData.Success;
                var result = baseService.GetList<dynamic>("dbo.sp_Report_DetailPayable", param: param);

                response.ActionResult = ActionResultData.Success;
                response.ActionData = result;
            }
            catch (Exception ex)
            {
                response.ActionResult = ActionResultData.Failed;
                response.ErrorData = new ErrorDataModel(ex);
            }
            return response;
        }
        public ResponseData GetReportData(long companyId, long userId, string code, string json)
        {
            ResponseData response = new ResponseData();
            //Get Report Procedure
            var param = new DynamicParameters();
            String procedureName = "";
            try
            {
                param = new DynamicParameters();
                param.Add("@Code", dbType: DbType.String, value: code, direction: ParameterDirection.Input);

                var result = baseService.GetSingle<dynamic>("dbo.sp_GetProcedure_ByName", param: param);

                procedureName= result.procedureName;
            }
            catch (Exception ex)
            {
                
            }

            if (procedureName.IsNullOrEmpty())
            {
                response.ActionResult = ActionResultData.Failed;
                response.ErrorData = new ErrorDataModel("NO_REPORT", "Không có thông tin báo cáo");
            } else
            {
                try
                {
                    param = new DynamicParameters();
                    param.Add("@CompanyId", dbType: DbType.Int64, value: companyId, direction: ParameterDirection.Input);
                    param.Add("@UserId", dbType: DbType.Int64, value: userId, direction: ParameterDirection.Input);
                    param.Add("@Json", dbType: DbType.String, value: json, direction: ParameterDirection.Input);

                    response.ActionResult = ActionResultData.Success;
                    var result = baseService.GetList<dynamic>(procedureName, param: param);

                    response.ActionResult = ActionResultData.Success;
                    response.ActionData = result;
                }
                catch (Exception ex)
                {
                    response.ActionResult = ActionResultData.Failed;
                    response.ErrorData = new ErrorDataModel(ex);
                }
            }
            
            return response;
        }
        public ResponseData GetQuotationReportData(long companyId, long userId, string json)
        {
            ResponseData response = new ResponseData();
            try
            {
                var para = new DynamicParameters();
                para.Add("@CompanyId", dbType: DbType.Int64, value: companyId, direction: ParameterDirection.Input);
                para.Add("@UserId", dbType: DbType.Int64, value: userId, direction: ParameterDirection.Input);
                para.Add("@Json", dbType: DbType.String, value: json, direction: ParameterDirection.Input);

                var result = baseService.Connection().QueryMultiple("dbo.sp_Report_Quotation", param: para, commandType: CommandType.StoredProcedure);
                var head = result.ReadFirst<dynamic>();
                var detail = result.Read<dynamic>();
                head.details = detail;
                response.ActionResult = ActionResultData.Success;
                response.ActionData = head;
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
