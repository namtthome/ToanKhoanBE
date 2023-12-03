using Dapper;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using vn.com.pnsuite.common.dataaccess.interfaces;
using vn.com.pnsuite.common.models;
using vn.com.pnsuite.toankhoan.dataaccess.Interfaces.Categories;
using vn.com.pnsuite.toankhoan.models.Pricebook;

namespace vn.com.pnsuite.toankhoan.dataaccess.Repositories.Categories
{
    public class PricebookService : IPricebookService
    {

        private readonly IBaseService baseService;
        private readonly AppSettings appSettings;
        public PricebookService(IOptions<AppSettings> appSettings, IBaseService baseService)
        {
            this.appSettings = appSettings.Value;
            this.baseService = baseService;
        }


        public async Task<ResponseData> GetAllStatusBySearchAsync(long companyId, long userId, string json)
        {
            ResponseData response = new ResponseData();
            try
            {
                var param = new DynamicParameters();
                param.Add("@CompanyId", dbType: DbType.Int64, value: companyId, direction: ParameterDirection.Input);
                param.Add("@UserId", dbType: DbType.Int64, value: userId, direction: ParameterDirection.Input);
                param.Add("@Json", dbType: DbType.String, value: json, direction: ParameterDirection.Input);

                response.ActionResult = ActionResultData.Success;
                response.ActionData = await baseService.GetListAsync<dynamic>("dbo.sp_Pricebook_Status_GetList", param);
            }
            catch (Exception ex)
            {
                response.ActionResult = ActionResultData.Failed;
                response.ErrorData = new ErrorDataModel(ex);
            }
            return response;
        }
        public async Task<ResponseData> DeletePricebookDetail(long userId, DeletePricebookDetailModel request)
        {
            ResponseData response = new ResponseData();
            try
            {
                var param = BuildDeleteDetailDynamicParameters(userId, request);
                await baseService.ExecuteAsync("sp_Category_Pricebook_DeleteDetail", param);
                response.ActionResult = ActionResultData.Success;
            }
            catch (Exception ex)
            {
                response.ActionResult = ActionResultData.Failed;
                response.ErrorData = new ErrorDataModel(ex);
            }

            return response;
        }
        public async Task<ResponseData> AddOrUpdatePricebookDetail(long userId, AddOrUpdatePricebookDetailModel request)
        {
            ResponseData response = new ResponseData();
            try
            {
                var param = BuildAddDetailDynamicParameters(userId, request);
                await baseService.ExecuteAsync("sp_Category_Pricebook_AddOrUpdateDetail", param);
                response.ActionResult = ActionResultData.Success;
            }
            catch (Exception ex)
            {
                response.ActionResult = ActionResultData.Failed;
                response.ErrorData = new ErrorDataModel(ex);
            }

            return response;
        }
        public async Task<ResponseData> Delete(long userId, int id)
        {
            ResponseData response = new ResponseData();
            try
            {
                var param = new DynamicParameters();
                param.Add("@UserId", dbType: DbType.Int64, value: userId, direction: ParameterDirection.Input);
                param.Add("@Id", dbType: DbType.Int32, value: id, direction: ParameterDirection.Input);
                await baseService.ExecuteAsync("sp_Category_Product_Delete", param);
                response.ActionResult = ActionResultData.Success;
            }
            catch (Exception ex)
            {
                response.ActionResult = ActionResultData.Failed;
                response.ErrorData = new ErrorDataModel(ex);
            }

            return response;
        }
        public async Task<ResponseData> GetDetail(long userId, int id)
        {
            ResponseData response = new ResponseData();
            try
            {
                var param = new DynamicParameters();
                param.Add("@UserId", dbType: DbType.Int64, value: userId, direction: ParameterDirection.Input);
                param.Add("@Id", dbType: DbType.Int64, value: id, direction: ParameterDirection.Input);
                using IDbConnection db = baseService.Connection();
                var result = await db.QueryMultipleAsync("sp_Category_Pricebook_GetByDetail", param, commandType: CommandType.StoredProcedure);
                var pricebook = result.Read<dynamic>().FirstOrDefault();
                if (pricebook != null)
                {
                    pricebook.PricebookDetails = result.Read<dynamic>().AsList();
                    response.ActionData = pricebook;
                    response.ActionResult = ActionResultData.Success;
                }
            }
            catch (Exception ex)
            {
                response.ActionResult = ActionResultData.Failed;
                response.ErrorData = new ErrorDataModel(ex);
            }

            return response;
        }

        public async Task<ResponseData> UpdateAsync(long userId, PricebookUpdateModel model)
        {
            ResponseData response = new ResponseData();
            try
            {
                var param = new DynamicParameters();
                param.Add("@UserId", dbType: DbType.Int64, value: userId, direction: ParameterDirection.Input);
                param.Add("@Json", dbType: DbType.String, value: model.JsonData, direction: ParameterDirection.Input);
                await baseService.ExecuteAsync("sp_Category_Pricebook_Update", param);
                response.ActionResult = ActionResultData.Success;
            }
            catch (Exception ex)
            {
                response.ActionResult = ActionResultData.Failed;
                response.ErrorData = new ErrorDataModel(ex);
            }

            return response;
        }

        public async Task<ResponseData> CreateAsync(long userId, PricebookCreateModel model)
        {
            ResponseData response = new ResponseData();
            try
            {
                var param = BuildCreateDynamicParameters(userId, model);
                await baseService.ExecuteAsync("sp_Category_Pricebook_Create", param);
                response.ActionResult = ActionResultData.Success;
            }
            catch (Exception ex)
            {
                response.ActionResult = ActionResultData.Failed;
                response.ErrorData = new ErrorDataModel(ex);
            }

            return response;
        }

        #region private methods

        private DynamicParameters BuildAddDetailDynamicParameters(long userId, AddOrUpdatePricebookDetailModel model)
        {
            var param = new DynamicParameters();
            param.Add("@UserId", dbType: DbType.Int64, value: userId, direction: ParameterDirection.Input);
            param.Add("@Json", dbType: DbType.String, value: model.JsonData, direction: ParameterDirection.Input);
            return param;
        }

        private DynamicParameters BuildDeleteDetailDynamicParameters(long userId, DeletePricebookDetailModel model)
        {
            var param = new DynamicParameters();
            param.Add("@UserId", dbType: DbType.Int64, value: userId, direction: ParameterDirection.Input);
            param.Add("@Json", dbType: DbType.String, value: model.JsonData, direction: ParameterDirection.Input);
            return param;
        }

        private DynamicParameters BuildCreateDynamicParameters(long userId, PricebookCreateModel model)
        {
            var param = new DynamicParameters();
            param.Add("@UserId", dbType: DbType.Int64, value: userId, direction: ParameterDirection.Input);
            param.Add("@Json", dbType: DbType.String, value: model.JsonData, direction: ParameterDirection.Input);
            return param;
        }

        private DynamicParameters BuildGetListDynamicParameters(long userId, List<CommonRequestValue> values)
        {
            var param = new DynamicParameters();
            string searchKey = string.Empty;
            string createdDateFromDate = string.Empty;
            string createdDateToDate = string.Empty;

            string validTimeFromDate = string.Empty;
            string validTimeToToDate = string.Empty;
            int status = 0;

            if (values != null && values.Count > 0)
            {
                CommonRequestValue searchkeyInfo = values.FirstOrDefault(item => item.Code.Equals("Search"));
                if (searchkeyInfo != null && searchkeyInfo.Value != null)
                {
                    searchKey = searchkeyInfo.Value.ToString();
                }

                CommonRequestValue fromDate = values.FirstOrDefault(item => item.Code.Equals("CreatedDateFromDate"));
                if (fromDate != null && fromDate.Value != null)
                {
                    createdDateFromDate = fromDate.Value.ToString();
                }

                CommonRequestValue toDate = values.FirstOrDefault(item => item.Code.Equals("CreatedDateToDate"));
                if (toDate != null && toDate.Value != null)
                {
                    createdDateToDate = toDate.Value.ToString();
                }

                CommonRequestValue validFromDate = values.FirstOrDefault(item => item.Code.Equals("ValidTimeFromDate"));
                if (validFromDate != null && validFromDate.Value != null)
                {
                    validTimeFromDate = validFromDate.Value.ToString();
                }

                CommonRequestValue validToDate = values.FirstOrDefault(item => item.Code.Equals("ValidTimeToDate"));
                if (validToDate != null && validToDate.Value != null)
                {
                    validTimeToToDate = validToDate.Value.ToString();
                }

                CommonRequestValue statusRequest = values.FirstOrDefault(item => item.Code.Equals("Status"));
                if (statusRequest != null && statusRequest.Value != null)
                {
                    status = (int)statusRequest.Value;
                }
            }

            param.Add("@UserId", dbType: DbType.Int64, value: userId, direction: ParameterDirection.Input);
            param.Add("@SearchValue", dbType: DbType.String, value: searchKey, direction: ParameterDirection.Input);
            param.Add("@CreatedDateFromDate", dbType: DbType.DateTime2, value: createdDateFromDate, direction: ParameterDirection.Input);
            param.Add("@CreatedDateToDate", dbType: DbType.DateTime2, value: createdDateToDate, direction: ParameterDirection.Input);
            param.Add("@ValidTimeFromDate", dbType: DbType.DateTime2, value: validTimeFromDate, direction: ParameterDirection.Input);
            param.Add("@ValidTimeToToDate", dbType: DbType.DateTime2, value: validTimeToToDate, direction: ParameterDirection.Input);
            param.Add("@Status", dbType: DbType.Int32, value: status, direction: ParameterDirection.Input);
            return param;
        }
        private List<Pricebook> BuildOutputData(List<Pricebook> pricebookLst, List<PricebookDetail> pricebookDetailLst)
        {
            foreach (var pricebookItem in pricebookLst)
            {
                pricebookItem.PricebookDetails = pricebookDetailLst
                    .Where(item => item.PricebookId == pricebookItem.Id
                    && item.CompanyId == pricebookItem.CompanyId)
                    .ToList();

            }
            return pricebookLst;
        }




        public async Task<ResponseData> GetAllBySearchAsync(long companyId, long userId, string json)
        {
            ResponseData response = new ResponseData();
            try
            {
                var param = new DynamicParameters();
                param.Add("@CompanyId", dbType: DbType.Int64, value: companyId, direction: ParameterDirection.Input);
                param.Add("@UserId", dbType: DbType.Int64, value: userId, direction: ParameterDirection.Input);
                param.Add("@Json", dbType: DbType.String, value: json, direction: ParameterDirection.Input);
                
                using IDbConnection db = baseService.Connection();
                var result = await db.QueryMultipleAsync("sp_Category_Pricebook_GetList", param, commandType: CommandType.StoredProcedure);
                var pricebookLst = result.Read<dynamic>().AsList();
                
                response.ActionData = pricebookLst;
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
                param.Add("@Json", dbType: DbType.String, value: json, direction: ParameterDirection.Input);
                param.Add("@Code", dbType: DbType.String, value: json, direction: ParameterDirection.Output);
                await baseService.ExecuteAsync("sp_Category_Pricebook_Create", param);
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

        public async Task<ResponseData> UpdateAsync(long companyId, long userId, string json)
        {
            ResponseData response = new ResponseData();
            try
            {
                var param = new DynamicParameters();
                param.Add("@CompanyId", dbType: DbType.Int64, value: companyId, direction: ParameterDirection.Input);
                param.Add("@UserId", dbType: DbType.Int64, value: userId, direction: ParameterDirection.Input);
                param.Add("@Json", dbType: DbType.String, value: json, direction: ParameterDirection.Input);
                await baseService.ExecuteAsync("sp_Category_Pricebook_Update", param);
                response.ActionResult = ActionResultData.Success;
            }
            catch (Exception ex)
            {
                response.ActionResult = ActionResultData.Failed;
                response.ErrorData = new ErrorDataModel(ex);
            }

            return response;
        }

        public async Task<ResponseData> GetDetail(long companyId, long userId, string json)
        {
            ResponseData response = new ResponseData();
            try
            {
                var param = new DynamicParameters();
                param.Add("@CompanyId", dbType: DbType.Int64, value: companyId, direction: ParameterDirection.Input);
                param.Add("@UserId", dbType: DbType.Int64, value: userId, direction: ParameterDirection.Input);
                param.Add("@Json", dbType: DbType.String, value: json, direction: ParameterDirection.Input);
                using IDbConnection db = baseService.Connection();
                var result = await db.QueryMultipleAsync("sp_Category_Pricebook_GetByDetail", param, commandType: CommandType.StoredProcedure);
                var pricebook = result.Read<dynamic>().FirstOrDefault();
                var pricebookDetailLst = result.Read<dynamic>().AsList();
                pricebook.pricebookDetails = pricebookDetailLst;
                response.ActionData = pricebook;
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
                await baseService.ExecuteAsync("sp_Category_Pricebook_Delete", param);
                response.ActionResult = ActionResultData.Success;
            }
            catch (Exception ex)
            {
                response.ActionResult = ActionResultData.Failed;
                response.ErrorData = new ErrorDataModel(ex);
            }

            return response;
        }

        public Task<ResponseData> AddOrUpdatePricebookDetail(long companyId, long userId, string json)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseData> DeletePricebookDetail(long companyId, long userId, string json)
        {
            throw new NotImplementedException();
        }


        #endregion



    }
}
