using System.Collections.Generic;
using System.Threading.Tasks;
using vn.com.pnsuite.common.models;
using vn.com.pnsuite.toankhoan.models.Products.DTOs;

namespace vn.com.pnsuite.toankhoan.dataaccess.Interfaces.Categories
{
    public interface IProductService
    {
        Task<ResponseData> GetAllBySearchAsync(long companyId, long userId, string json);
        Task<ResponseData> GetDetail(long companyId, long userId, string json);
        Task<ResponseData> Delete(long companyId, long userId, string json);
        Task<ResponseData> GetExtraInfo(long companyId, long userId, string json);
        Task<ResponseData> CreateOrUpdateAsync(long companyId, long userId, string json);
        Task<ResponseData> GetPrice(long companyId, long userId, string json);
        Task<ResponseData> GetAllInventoryBySearch(long companyId, long userId, string json);
        Task<ResponseData> GetCopyData(long companyId, long userId, string json);
    }
}
