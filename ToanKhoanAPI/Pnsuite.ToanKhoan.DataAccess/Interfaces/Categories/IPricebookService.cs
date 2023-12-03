using System.Threading.Tasks;
using vn.com.pnsuite.common.models;

namespace vn.com.pnsuite.toankhoan.dataaccess.Interfaces.Categories
{
    public interface IPricebookService
    {
        Task<ResponseData> GetAllBySearchAsync(long companyId, long userId, string json);
        Task<ResponseData> GetAllStatusBySearchAsync(long companyId, long userId, string json);

        Task<ResponseData> CreateAsync(long companyId, long userId, string json);
        Task<ResponseData> UpdateAsync(long companyId, long userId, string json);
        Task<ResponseData> GetDetail(long companyId, long userId, string json);
        Task<ResponseData> Delete(long companyId, long userId, string json);
        Task<ResponseData> AddOrUpdatePricebookDetail(long companyId, long userId, string json);
        Task<ResponseData> DeletePricebookDetail(long companyId, long userId, string json);

    }
}
