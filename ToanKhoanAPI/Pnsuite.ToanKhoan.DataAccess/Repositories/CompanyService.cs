using Dapper;
using Microsoft.Extensions.Options;
using System;
using System.Data;
using vn.com.pnsuite.common.dataaccess.interfaces;
using vn.com.pnsuite.common.models;
using vn.com.pnsuite.hrm.dataaccess.interfaces;
using vn.com.pnsuite.hrm.models.company;

namespace vn.com.pnsuite.hrm.dataaccess.repositories
{
    public class CompanyService : ICompanyService
    {
        private readonly IBaseService baseService;
        private readonly AppSettings appSettings;
        public CompanyService(IOptions<AppSettings> appSettings, IBaseService baseService)
        {
            this.appSettings = appSettings.Value;
            this.baseService = baseService;
        }

        public ResponseData GetByTaxcode(string taxcode)
        {
            ResponseData response = new ResponseData();
            try
            {
                var param = new DynamicParameters();
                param.Add("@Taxcode", dbType: DbType.String, value: taxcode, direction: ParameterDirection.Input);

                response.ActionResult = ActionResultData.Success;
                response.ActionData = baseService.GetSingle<CompanyModel>("sp_Company_GetByTaxcode", param);
            }
            catch (Exception ex)
            {
                response.ActionResult = ActionResultData.Failed;
                response.ErrorData = new ErrorDataModel(ex);
            }
            return response;
        }

        public ResponseData Update(CompanyModel value, long userId)
        {
            ResponseData response = new ResponseData();
            try
            {
                var param = new DynamicParameters();
                param.Add("@Id", dbType: DbType.Int64, value: value.Id, direction: ParameterDirection.Input);
                param.Add("@Taxcode", dbType: DbType.String, value: value.Taxcode, direction: ParameterDirection.Input);
                param.Add("@Name", dbType: DbType.String, value: value.CompanyName, direction: ParameterDirection.Input);
                param.Add("@Address", dbType: DbType.String, value: value.CompanyAddress, direction: ParameterDirection.Input);
                param.Add("@Ward", dbType: DbType.String, value: value.Ward, direction: ParameterDirection.Input);
                param.Add("@District", dbType: DbType.String, value: value.District, direction: ParameterDirection.Input);
                param.Add("@Province", dbType: DbType.String, value: value.Province, direction: ParameterDirection.Input);
                param.Add("@Tel", dbType: DbType.String, value: value.CompanyTel, direction: ParameterDirection.Input);
                param.Add("@LineOfBusiness", dbType: DbType.String, value: value.BusinessLine, direction: ParameterDirection.Input);
                param.Add("@Representative", dbType: DbType.String, value: value.Representative, direction: ParameterDirection.Input);
                param.Add("@RepresentativePosition", dbType: DbType.String, value: value.RepresentativePosition, direction: ParameterDirection.Input);
                param.Add("@RepresentativeTel", dbType: DbType.String, value: value.RepresentativeTel, direction: ParameterDirection.Input);
                param.Add("@RepresentativeAdd", dbType: DbType.String, value: value.RepresentativeAddress, direction: ParameterDirection.Input);
                param.Add("@TaxAuthorityId", dbType: DbType.Int32, value: value.TaxAuthorityId, direction: ParameterDirection.Input);
                param.Add("@UserId", dbType: DbType.Int64, value: userId, direction: ParameterDirection.Input);

                response.ActionResult = ActionResultData.Success;
                baseService.Update("sp_Company_InsertOrUpdate", param);
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
