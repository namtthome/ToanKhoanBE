using vn.com.pnsuite.common.models;
using vn.com.pnsuite.hrm.models.company;

namespace vn.com.pnsuite.hrm.dataaccess.interfaces
{
    public interface ICompanyService
    {
        ResponseData GetByTaxcode(string taxcode);
        ResponseData Update(CompanyModel value, long userId);
    }
}
