using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using vn.com.pnsuite.common.dataaccess.interfaces;
using vn.com.pnsuite.common.models;
using vn.com.pnsuite.toankhoan.dataaccess.interfaces;

namespace vn.com.pnsuite.toankhoan.dataaccess.repositories
{
    public class VersionService : IVersionService
    {
        private readonly IBaseService baseService;
        private readonly AppSettings appSettings;
        public VersionService(IOptions<AppSettings> appSettings, IBaseService baseService)
        {
            this.appSettings = appSettings.Value;
            this.baseService = baseService;
        }
        public ResponseData getAll()
        {
            ResponseData response = new ResponseData();
            try
            {
                response.ActionResult = ActionResultData.Success;
                response.ActionData = baseService.GetList<Object>("sp_Version_GetList", null);
            }
            catch (Exception ex)
            {
                response.ActionResult = ActionResultData.Failed;
                response.ErrorData = new ErrorDataModel(ex) ;
            }
            return response;
        }
    }
}
