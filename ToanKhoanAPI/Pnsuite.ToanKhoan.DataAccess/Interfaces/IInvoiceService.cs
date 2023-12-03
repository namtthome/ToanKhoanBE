using System.Threading.Tasks;
using vn.com.pnsuite.common.models;

namespace vn.com.pnsuite.toankhoan.dataaccess.Interfaces
{
    public interface IInvoiceService
    {
        #region Invoice
        Task<ResponseData> GetAllInvoiceBySearchAsync(long userId, long companyId, string search);
        Task<ResponseData> GetDetailInvoice(long companyId, long userId, string json);
        Task<ResponseData> CreateInvoiceAsync(long companyId, long userId, string json);
        Task<ResponseData> UpdateInvoiceAsync(long companyId, long userId, string json);
        Task<ResponseData> DeleteInvoiceAsync(long companyId, long userId, string json);

        #endregion

        #region Return


        Task<ResponseData> GetAllReturnBySearchAsync(long userId, long companyId, string search);
        Task<ResponseData> CreateReturnAsync(long companyId, long userId, string json);
        Task<ResponseData> UpdateReturnAsync(long companyId, long userId, string json);
        Task<ResponseData> GetReturnDetailAsync(long companyId, long userId, string json);
        Task<ResponseData> DeleteReturnAsync(long companyId, long userId, string json);
        Task<ResponseData> GetCopyInvoice(long companyId, long userId, string json);
        Task<ResponseData> GetCopyDataReturnDetailAsync(long companyId, long userId, string json);
        Task<ResponseData> GetNewInvoiceFromQuotation(long companyId, long userId, string json);
        #endregion
    }
}
