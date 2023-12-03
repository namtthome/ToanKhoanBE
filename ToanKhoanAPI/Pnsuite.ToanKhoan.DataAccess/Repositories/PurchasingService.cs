using Dapper;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using vn.com.pnsuite.common.dataaccess.interfaces;
using vn.com.pnsuite.common.models;
using vn.com.pnsuite.hrm.models.company;
using vn.com.pnsuite.toankhoan.dataaccess.Interfaces;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace vn.com.pnsuite.toankhoan.dataaccess.Repositories
{
    public class PurchasingService : IPurchasingService
    {
        private readonly IBaseService baseService;
        private readonly AppSettings appSettings;
        public PurchasingService(IOptions<AppSettings> appSettings, IBaseService baseService)
        {
            this.appSettings = appSettings.Value;
            this.baseService = baseService;
        }
        #region order
        public ResponseData GetOrderList(long companyId, long userId, string json)
        {
            ResponseData response = new ResponseData();
            try
            {
                var param = new DynamicParameters();
                param.Add("@CompanyId", dbType: DbType.Int64, value: companyId, direction: ParameterDirection.Input);
                param.Add("@UserId", dbType: DbType.Int64, value: userId, direction: ParameterDirection.Input);
                param.Add("@Json", dbType: DbType.String, value: json, direction: ParameterDirection.Input);

                response.ActionResult = ActionResultData.Success;
                response.ActionData = baseService.GetList<dynamic>("dbo.sp_PurchaseOrder_GetList", param);
            }
            catch (Exception ex)
            {
                response.ActionResult = ActionResultData.Failed;
                response.ErrorData = new ErrorDataModel(ex);
            }
            return response;
        }
        public ResponseData GetOrderById(long companyId, long userId, string json)
        {
            ResponseData response = new ResponseData();
            try
            {
                var param = new DynamicParameters();
                param.Add("@CompanyId", dbType: DbType.Int64, value: companyId, direction: ParameterDirection.Input);
                param.Add("@UserId", dbType: DbType.Int64, value: userId, direction: ParameterDirection.Input);
                param.Add("@Json", dbType: DbType.String, value: json, direction: ParameterDirection.Input);

                response.ActionResult = ActionResultData.Success;
                var result = baseService.Connection().QueryMultiple("dbo.sp_PurchaseOrder_Get", param: param, commandType: CommandType.StoredProcedure);
                var header = result.ReadFirst<dynamic>();
                var details = result.Read<dynamic>();
                var extra = result.Read<dynamic>();

                foreach (var item in details)
                {
                    item.extraInfo = extra.Where(e => e.purchaseOrderDetailId == item.id).ToList();
                }

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
        public ResponseData DeleteOrderById(long companyId, long userId, string json)
        {
            ResponseData response = new ResponseData();
            try
            {
                var param = new DynamicParameters();
                param.Add("@CompanyId", dbType: DbType.Int64, value: companyId, direction: ParameterDirection.Input);
                param.Add("@UserId", dbType: DbType.Int64, value: userId, direction: ParameterDirection.Input);
                param.Add("@Json", dbType: DbType.String, value: json, direction: ParameterDirection.Input);

                response.ActionResult = ActionResultData.Success;
                baseService.Update("dbo.sp_PurchaseOrder_Delete", param);
            }
            catch (Exception ex)
            {
                response.ActionResult = ActionResultData.Failed;
                response.ErrorData = new ErrorDataModel(ex);
            }
            return response;
        }
        public ResponseData UpdateOrder(long companyId, long userId, string json)
        {
            ResponseData response = new ResponseData();
            try
            {
                var param = new DynamicParameters();
                param.Add("@CompanyId", dbType: DbType.Int64, value: companyId, direction: ParameterDirection.Input);
                param.Add("@UserId", dbType: DbType.Int64, value: userId, direction: ParameterDirection.Input);
                param.Add("@Json", dbType: DbType.String, value: json, direction: ParameterDirection.Input);
                param.Add("@Code", dbType: DbType.String, value: "", direction: ParameterDirection.Output);

                response.ActionResult = ActionResultData.Success;
                baseService.Update("dbo.sp_PurchaseOrder_InsertOrUpdate", param);
                response.ActionData = new { code = param.Get<String>("@Code") };
            }
            catch (Exception ex)
            {
                response.ActionResult = ActionResultData.Failed;
                response.ErrorData = new ErrorDataModel(ex);
            }
            return response;
        }
        #endregion

        #region voucher
        public ResponseData GetVoucherList(long companyId, long userId, string json)
        {
            ResponseData response = new ResponseData();
            try
            {
                var param = new DynamicParameters();
                param.Add("@CompanyId", dbType: DbType.Int64, value: companyId, direction: ParameterDirection.Input);
                param.Add("@UserId", dbType: DbType.Int64, value: userId, direction: ParameterDirection.Input);
                param.Add("@Json", dbType: DbType.String, value: json, direction: ParameterDirection.Input);

                response.ActionResult = ActionResultData.Success;
                response.ActionData = baseService.GetList<dynamic>("dbo.sp_PurchaseVoucher_GetList", param);
            }
            catch (Exception ex)
            {
                response.ActionResult = ActionResultData.Failed;
                response.ErrorData = new ErrorDataModel(ex);
            }
            return response;
        }
        public ResponseData GetVoucherById(long companyId, long userId, string json)
        {
            ResponseData response = new ResponseData();
            try
            {
                var param = new DynamicParameters();
                param.Add("@CompanyId", dbType: DbType.Int64, value: companyId, direction: ParameterDirection.Input);
                param.Add("@UserId", dbType: DbType.Int64, value: userId, direction: ParameterDirection.Input);
                param.Add("@Json", dbType: DbType.String, value: json, direction: ParameterDirection.Input);

                response.ActionResult = ActionResultData.Success;
                var result = baseService.Connection().QueryMultiple("dbo.sp_PurchaseVoucher_Get", param: param, commandType: CommandType.StoredProcedure);
                var header = result.ReadFirst<dynamic>();
                var details = result.Read<dynamic>();
                var extra = result.Read<dynamic>();
                var po = result.Read<dynamic>();

                foreach (var item in details)
                {
                    item.extraInfo = extra.Where(e => e.purchaseVoucherDetailId == item.id).ToList();
                }

                if (header != null)
                {
                    header.details = details;
                    header.po = po;
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
        public ResponseData GetCopyVoucher(long companyId, long userId, string json)
        {
            ResponseData response = new ResponseData();
            try
            {
                var param = new DynamicParameters();
                param.Add("@CompanyId", dbType: DbType.Int64, value: companyId, direction: ParameterDirection.Input);
                param.Add("@UserId", dbType: DbType.Int64, value: userId, direction: ParameterDirection.Input);
                param.Add("@Json", dbType: DbType.String, value: json, direction: ParameterDirection.Input);

                response.ActionResult = ActionResultData.Success;
                var result = baseService.Connection().QueryMultiple("dbo.sp_PurchaseVoucher_GetCopyData", param: param, commandType: CommandType.StoredProcedure);
                var header = result.ReadFirst<dynamic>();
                var details = result.Read<dynamic>();
                var po = result.Read<dynamic>();

                if (header != null)
                {
                    header.details = details;
                    header.po = po;
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
        public ResponseData UpdateVoucher(long companyId, long userId, string json)
        {
            ResponseData response = new ResponseData();
            try
            {
                var param = new DynamicParameters();
                param.Add("@CompanyId", dbType: DbType.Int64, value: companyId, direction: ParameterDirection.Input);
                param.Add("@UserId", dbType: DbType.Int64, value: userId, direction: ParameterDirection.Input);
                param.Add("@Json", dbType: DbType.String, value: json, direction: ParameterDirection.Input);
                param.Add("@Code", dbType: DbType.String, value: "", direction: ParameterDirection.Output);

                response.ActionResult = ActionResultData.Success;
                baseService.Update("dbo.sp_PurchaseVoucher_InsertOrUpdate", param);

                response.ActionData = new { code = param.Get<String>("@Code") };
            }
            catch (Exception ex)
            {
                response.ActionResult = ActionResultData.Failed;
                response.ErrorData = new ErrorDataModel(ex);
            }
            return response;
        }
        public ResponseData DeleteVoucherById(long companyId, long userId, string json)
        {
            ResponseData response = new ResponseData();
            try
            {
                var param = new DynamicParameters();
                param.Add("@CompanyId", dbType: DbType.Int64, value: companyId, direction: ParameterDirection.Input);
                param.Add("@UserId", dbType: DbType.Int64, value: userId, direction: ParameterDirection.Input);
                param.Add("@Json", dbType: DbType.String, value: json, direction: ParameterDirection.Input);

                response.ActionResult = ActionResultData.Success;
                baseService.Update("dbo.sp_PurchaseVoucher_Delete", param);
            }
            catch (Exception ex)
            {
                response.ActionResult = ActionResultData.Failed;
                response.ErrorData = new ErrorDataModel(ex);
            }
            return response;
        }
        #endregion

        #region return
        public ResponseData GetReturnList(long companyId, long userId, string json)
        {
            ResponseData response = new ResponseData();
            try
            {
                var param = new DynamicParameters();
                param.Add("@CompanyId", dbType: DbType.Int64, value: companyId, direction: ParameterDirection.Input);
                param.Add("@UserId", dbType: DbType.Int64, value: userId, direction: ParameterDirection.Input);
                param.Add("@Json", dbType: DbType.String, value: json, direction: ParameterDirection.Input);

                response.ActionResult = ActionResultData.Success;
                response.ActionData = baseService.GetList<dynamic>("dbo.sp_PurchaseReturn_GetList", param);
            }
            catch (Exception ex)
            {
                response.ActionResult = ActionResultData.Failed;
                response.ErrorData = new ErrorDataModel(ex);
            }
            return response;
        }
        public ResponseData GetReturnById(long companyId, long userId, string json)
        {
            ResponseData response = new ResponseData();
            try
            {
                var param = new DynamicParameters();
                param.Add("@CompanyId", dbType: DbType.Int64, value: companyId, direction: ParameterDirection.Input);
                param.Add("@UserId", dbType: DbType.Int64, value: userId, direction: ParameterDirection.Input);
                param.Add("@Json", dbType: DbType.String, value: json, direction: ParameterDirection.Input);

                response.ActionResult = ActionResultData.Success;
                var result = baseService.Connection().QueryMultiple("dbo.sp_PurchaseReturn_Get", param: param, commandType: CommandType.StoredProcedure);
                var header = result.ReadFirst<dynamic>();
                var details = result.Read<dynamic>();
                var extra = result.Read<dynamic>();
                var pv = result.Read<dynamic>();

                foreach (var item in details)
                {
                    item.extraInfo = extra.Where(e => e.purchaseReturnDetailId == item.id).ToList();
                }

                if (header != null)
                {
                    header.details = details;
                    header.pv = pv;
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
        public ResponseData GetCopyDataReturnById(long companyId, long userId, string json)
        {
            ResponseData response = new ResponseData();
            try
            {
                var param = new DynamicParameters();
                param.Add("@CompanyId", dbType: DbType.Int64, value: companyId, direction: ParameterDirection.Input);
                param.Add("@UserId", dbType: DbType.Int64, value: userId, direction: ParameterDirection.Input);
                param.Add("@Json", dbType: DbType.String, value: json, direction: ParameterDirection.Input);

                response.ActionResult = ActionResultData.Success;
                var result = baseService.Connection().QueryMultiple("dbo.sp_PurchaseReturn_GetCopyData", param: param, commandType: CommandType.StoredProcedure);
                var header = result.ReadFirst<dynamic>();
                var details = result.Read<dynamic>();
                var pv = result.Read<dynamic>();

                if (header != null)
                {
                    header.details = details;
                    header.pv = pv;
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
        public ResponseData UpdateReturn(long companyId, long userId, string json)
        {
            ResponseData response = new ResponseData();
            try
            {
                var param = new DynamicParameters();
                param.Add("@CompanyId", dbType: DbType.Int64, value: companyId, direction: ParameterDirection.Input);
                param.Add("@UserId", dbType: DbType.Int64, value: userId, direction: ParameterDirection.Input);
                param.Add("@Json", dbType: DbType.String, value: json, direction: ParameterDirection.Input);
                param.Add("@Code", dbType: DbType.String, value: "", direction: ParameterDirection.Output);

                response.ActionResult = ActionResultData.Success;
                baseService.Update("dbo.sp_PurchaseReturn_InsertOrUpdate", param);
                response.ActionData = new { code = param.Get<String>("@Code") };
            }
            catch (Exception ex)
            {
                response.ActionResult = ActionResultData.Failed;
                response.ErrorData = new ErrorDataModel(ex);
            }
            return response;
        }
        public ResponseData DeleteReturnById(long companyId, long userId, string json)
        {
            ResponseData response = new ResponseData();
            try
            {
                var param = new DynamicParameters();
                param.Add("@CompanyId", dbType: DbType.Int64, value: companyId, direction: ParameterDirection.Input);
                param.Add("@UserId", dbType: DbType.Int64, value: userId, direction: ParameterDirection.Input);
                param.Add("@Json", dbType: DbType.String, value: json, direction: ParameterDirection.Input);

                response.ActionResult = ActionResultData.Success;
                baseService.Update("dbo.sp_PurchaseReturn_Delete", param);
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
