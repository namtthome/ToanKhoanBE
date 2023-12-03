using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using vn.com.pnsuite.common.models;

namespace vn.com.pnsuite.toankhoan.dataaccess.Interfaces
{
    public interface IPeriodService
    {
        Task<ResponseData> CreatePeriod(long companyId, long userId, string json);
        Task<ResponseData> GetPeriodList(long companyId, long userId, string json);
        Task<ResponseData> UpdatePeriodState(long companyId, long userId, string json);
    }
}
