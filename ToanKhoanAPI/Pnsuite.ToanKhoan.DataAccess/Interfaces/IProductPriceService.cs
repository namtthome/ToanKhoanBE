using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using vn.com.pnsuite.common.models;

namespace vn.com.pnsuite.toankhoan.dataaccess.interfaces
{
    public interface IProductPriceService
    {
        Task<ResponseData> GetAllSearchAsync(long companyId, long userId, string json);
        Task<ResponseData> GetProductPriceById(long companyId, long userId, string json);
        Task<ResponseData> CreateOrUpdateProductPriceAsync(long companyId, long userId, string json);
        Task<ResponseData> DeleteProductPriceAsync(long companyId, long userId, string json);

        Task<ResponseData> GetPriceConfigSearchAsync(long companyId, long userId, string json);
        Task<ResponseData> GetPriceConfigByIdAsync(long companyId, long userId, string json);
        Task<ResponseData> InsertOrUpdatePriceConfigAsync(long companyId, long userId, string json);
        Task<ResponseData> DeletePriceConfigAsync(long companyId, long userId, string json);
        
        Task<ResponseData> GetPriceConfigTypeSearchAsync(long companyId, long userId, string json);
        Task<ResponseData> CreatePriceByConfigTypeAsync(long companyId, long userId, string code, string json);
    }
}
