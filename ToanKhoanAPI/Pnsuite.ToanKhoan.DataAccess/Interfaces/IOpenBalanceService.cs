using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using vn.com.pnsuite.common.models;

namespace vn.com.pnsuite.toankhoan.dataaccess.Interfaces
{
    public interface IOpenBalanceService
    {
        Task<ResponseData> CreateOrUpdateDebtOpenBalance(long companyId, long userId, string json);
        Task<ResponseData> CreateOrUpdateInventoryOpenBalance(long companyId, long userId, string json);
        Task<ResponseData> DeleteDebtOpenBalance(long companyId, long userId, string json);
        Task<ResponseData> DeleteInventoryOpenBalance(long companyId, long userId, string json);
        Task<ResponseData> GetDebtOpenBalanceList(long companyId, long userId, string json);
        Task<ResponseData> GetDetailDebtOpenBalance(long companyId, long userId, string json);
        Task<ResponseData> GetDetailInventoryOpenBalance(long companyId, long userId, string json);
        Task<ResponseData> GetInventoryOpenBalanceList(long companyId, long userId, string json);
    }
}
