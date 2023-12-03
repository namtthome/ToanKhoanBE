using System.Threading.Tasks;
using vn.com.pnsuite.common.models;

namespace vn.com.pnsuite.toankhoan.dataaccess.Interfaces
{
    public interface IPartnerService
    {
        Task<ResponseData> GetPartnerList(long companyId, long userId, string searchType, string json);
    }
}
