using Dapper;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using vn.com.pnsuite.common.dataaccess.interfaces;
using vn.com.pnsuite.common.models;
using vn.com.pnsuite.toankhoan.dataaccess.Interfaces;

namespace vn.com.pnsuite.toankhoan.dataaccess.Repositories
{
    public class PartnerService: IPartnerService
    {
        private readonly IBaseService baseService;
        private readonly AppSettings appSettings;
        public PartnerService(IBaseService baseService, IOptions<AppSettings> appSettings)
        {
            this.baseService = baseService;
            this.appSettings = appSettings.Value;
        }
        public async Task<ResponseData> GetPartnerList(long companyId, long userId, string searchType, string json)
        {
            ResponseData response = new ResponseData();
            try
            {
                var param = new DynamicParameters();
                param.Add("@CompanyId", dbType: DbType.Int64, value: companyId, direction: ParameterDirection.Input);
                param.Add("@UserId", dbType: DbType.Int64, value: userId, direction: ParameterDirection.Input);
                param.Add("@SearchType", dbType: DbType.String, value: searchType, direction: ParameterDirection.Input);
                param.Add("@Json", dbType: DbType.String, value: json, direction: ParameterDirection.Input);

                response.ActionResult = ActionResultData.Success;
                response.ActionData = await baseService.GetListAsync<dynamic>("dbo.sp_Partner_GetList", param);
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
