using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using vn.com.pnsuite.common.models;

namespace vn.com.pnsuite.toankhoan.dataaccess.Interfaces
{
    public interface IAdjustService
    {
        Task<ResponseData> CreateOrUpdateAdjust(long companyId, long userId, string json);
        Task<ResponseData> DeleteAdjust(long companyId, long userId, string json);
        Task<ResponseData> GetAdjustList(long companyId, long userId, string json);
        Task<ResponseData> GetDetailAdjust(long companyId, long userId, string json);
    }
}
