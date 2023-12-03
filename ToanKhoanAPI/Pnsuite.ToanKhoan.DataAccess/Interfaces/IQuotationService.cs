using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using vn.com.pnsuite.common.models;

namespace vn.com.pnsuite.toankhoan.dataaccess.Interfaces
{
    public interface IQuotationService
    {
        Task<ResponseData> CreateQuotationAsync(long companyId, long userId, string json);
        Task<ResponseData> DeleteQuotationAsync(long companyId, long userId, string json);
        Task<ResponseData> GetAllQuotationBySearchAsync(long userId, long companyId, string search);
        Task<ResponseData> GetCopyQuotation(long companyId, long userId, string json);
        Task<ResponseData> GetQuotationDetail(long companyId, long userId, string json);
        Task<ResponseData> UpdateQuotationAsync(long companyId, long userId, string json);
    }
}
