using System.Threading.Tasks;
using vn.com.pnsuite.common.models;
using vn.com.pnsuite.toankhoan.models.Categories.Costs;

namespace vn.com.pnsuite.toankhoan.dataaccess.Interfaces.Categories
{
    public interface ICategoryCostService
    {
        Task<ResponseData> GetAllBySearchAsync(long companyId, long userId, string search);
        Task<ResponseData> GetDetail(long companyId, long userId, string search);
        Task<ResponseData> CreateAsync(long companyId, long userId, string json);
        Task<ResponseData> UpdateAsync(long companyId, long userId, string json);
        Task<ResponseData> Delete(long companyId, long userId, string json);

    }
}
