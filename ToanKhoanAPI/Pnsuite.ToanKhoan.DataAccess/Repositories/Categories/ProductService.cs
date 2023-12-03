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
using vn.com.pnsuite.toankhoan.models.Products.Domains;
using vn.com.pnsuite.toankhoan.models.Products.DTOs;

namespace vn.com.pnsuite.toankhoan.dataaccess.Repositories.Categories
{
    public class ProductService : IProductService
    {

        private readonly IBaseService baseService;
        private readonly AppSettings appSettings;
        public ProductService(IOptions<AppSettings> appSettings, IBaseService baseService)
        {
            this.appSettings = appSettings.Value;
            this.baseService = baseService;
        }
        public async Task<ResponseData> GetExtraInfo(long companyId, long userId, string json)
        {
            ResponseData response = new ResponseData();
            try
            {
                var param = new DynamicParameters();
                param.Add("@CompanyId", dbType: DbType.Int64, value: companyId, direction: ParameterDirection.Input);
                param.Add("@UserId", dbType: DbType.Int64, value: userId, direction: ParameterDirection.Input);
                param.Add("@Json", dbType: DbType.String, value: json, direction: ParameterDirection.Input);
                response.ActionResult = ActionResultData.Success;
                response.ActionData = await baseService.GetListAsync<ExtraInfo>("sp_Category_ExtraInfo_GetList", param: param);
            }
            catch (Exception ex)
            {
                response.ActionResult = ActionResultData.Failed;
                response.ErrorData = new ErrorDataModel(ex);
            }
            return response;
        }
        public async Task<ResponseData> GetPrice(long companyId, long userId, string json)
        {
            ResponseData response = new ResponseData();
            try
            {
                var param = new DynamicParameters();
                param.Add("@CompanyId", dbType: DbType.Int64, value: companyId, direction: ParameterDirection.Input);
                param.Add("@UserId", dbType: DbType.Int64, value: userId, direction: ParameterDirection.Input);
                param.Add("@Json", dbType: DbType.String, value: json, direction: ParameterDirection.Input);
                response.ActionResult = ActionResultData.Success;
                response.ActionData = await baseService.GetSingleAsync<dynamic>("sp_Price_GetByProduct", param: param);
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
                var result = await db.QueryMultipleAsync("sp_Category_Product_GetByDetail", param, commandType: CommandType.StoredProcedure);
                
                var product = result.Read<dynamic>().FirstOrDefault();
                if (product != null)
                {
                    product.productAttributes = result.Read<dynamic>().AsList();
                    product.productUnits = result.Read<dynamic>().AsList();
                    product.productHasExtraInfos = result.Read<dynamic>().AsList();

                    response.ActionData = product;
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
        public async Task<ResponseData> GetCopyData(long companyId, long userId, string json)
        {
            ResponseData response = new ResponseData();
            try
            {
                var param = new DynamicParameters();
                param.Add("@CompanyId", dbType: DbType.Int64, value: companyId, direction: ParameterDirection.Input);
                param.Add("@UserId", dbType: DbType.Int64, value: userId, direction: ParameterDirection.Input);
                param.Add("@Json", dbType: DbType.String, value: json, direction: ParameterDirection.Input);

                using IDbConnection db = baseService.Connection();
                var result = await db.QueryMultipleAsync("dbo.sp_Category_Product_CopyById", param, commandType: CommandType.StoredProcedure);

                var product = result.Read<dynamic>().FirstOrDefault();
                if (product != null)
                {
                    product.productAttributes = result.Read<dynamic>().AsList();
                    product.productUnits = result.Read<dynamic>().AsList();
                    product.productHasExtraInfos = result.Read<dynamic>().AsList();

                    response.ActionData = product;
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
        public async Task<ResponseData> CreateOrUpdateAsync(long companyId, long userId, string json)
        {
            ResponseData response = new ResponseData();
            try
            {
                var param = new DynamicParameters();
                param.Add("@CompanyId", dbType: DbType.Int64, value: companyId, direction: ParameterDirection.Input);
                param.Add("@UserId", dbType: DbType.Int64, value: userId, direction: ParameterDirection.Input);
                param.Add("@Json", dbType: DbType.String, value: json, direction: ParameterDirection.Input);
                param.Add("@Code", dbType: DbType.String, value: "", direction: ParameterDirection.Output);
                await baseService.ExecuteAsync("dbo.sp_Category_Product_InsertOrUpdate", param);
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
                var result = await db.QueryMultipleAsync("dbo.sp_Category_Product_GetList", param, commandType: CommandType.StoredProcedure);
                List<Product> productLst = result.Read<Product>().AsList();

                if (productLst != null && productLst.Count > 0)
                {
                    List<ProductAttribute> productAttributeLst = result.Read<ProductAttribute>().AsList();
                    List<ProductUnit> productUnitLst = result.Read<ProductUnit>().AsList();
                    List<ProductHasExtraInfo> productHasExtraInfoLst = result.Read<ProductHasExtraInfo>().AsList();
                    List<ProductInventory> productInventoryLst = result.Read<ProductInventory>().AsList();

                    foreach (var productItem in productLst)
                    {
                        productItem.ProductInventories = productInventoryLst
                            .Where(item => item.ProductId == productItem.Id
                            && item.CompanyId == productItem.CompanyId)
                            .ToList();
                        productItem.ProductAttributes = productAttributeLst
                            .Where(item => item.ProductId == productItem.Id
                            && item.CompanyId == productItem.CompanyId)
                            .ToList();

                        productItem.ProductHasExtraInfos = productHasExtraInfoLst
                            .Where(item => item.ProductId == productItem.Id
                            && item.CompanyId == productItem.CompanyId)
                            .ToList();
                        productItem.ProductUnits = productUnitLst
                            .Where(item => item.ProductId == productItem.Id
                            && item.CompanyId == productItem.CompanyId)
                            .ToList();
                    }
                }


                response.ActionData = productLst;
                response.ActionResult = ActionResultData.Success;
            }
            catch (Exception ex)
            {
                response.ActionResult = ActionResultData.Failed;
                response.ErrorData = new ErrorDataModel(ex);
            }
            return response;
        }
        public async Task<ResponseData> GetAllInventoryBySearch(long companyId, long userId, string json)
        {
            ResponseData response = new ResponseData();
            try
            {
                var param = new DynamicParameters();
                param.Add("@CompanyId", dbType: DbType.Int64, value: companyId, direction: ParameterDirection.Input);
                param.Add("@UserId", dbType: DbType.Int64, value: userId, direction: ParameterDirection.Input);
                param.Add("@Json", dbType: DbType.String, value: json, direction: ParameterDirection.Input);
                using IDbConnection db = baseService.Connection();
                var result = await db.QueryMultipleAsync("sp_Category_Product_Inventory_GetList", param, commandType: CommandType.StoredProcedure);
                List<Product> productLst = result.Read<Product>().AsList();

                if (productLst != null && productLst.Count > 0)
                {
                    List<ProductAttribute> productAttributeLst = result.Read<ProductAttribute>().AsList();
                    List<ProductUnit> productUnitLst = result.Read<ProductUnit>().AsList();
                    List<ProductHasExtraInfo> productHasExtraInfoLst = result.Read<ProductHasExtraInfo>().AsList();
                    List<ProductInventory> productInventoryLst = result.Read<ProductInventory>().AsList();

                    foreach (var productItem in productLst)
                    {
                        productItem.ProductInventories = productInventoryLst
                            .Where(item => item.ProductId == productItem.Id
                            && item.CompanyId == productItem.CompanyId)
                            .ToList();
                        productItem.ProductAttributes = productAttributeLst
                            .Where(item => item.ProductId == productItem.Id
                            && item.CompanyId == productItem.CompanyId)
                            .ToList();

                        productItem.ProductHasExtraInfos = productHasExtraInfoLst
                            .Where(item => item.ProductId == productItem.Id
                            && item.CompanyId == productItem.CompanyId)
                            .ToList();
                        productItem.ProductUnits = productUnitLst
                            .Where(item => item.ProductId == productItem.Id
                            && item.CompanyId == productItem.CompanyId)
                            .ToList();
                    }
                }


                response.ActionData = productLst;
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
