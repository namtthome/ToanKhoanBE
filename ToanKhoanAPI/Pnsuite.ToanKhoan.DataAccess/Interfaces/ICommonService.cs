using System;
using vn.com.pnsuite.common.models;

namespace vn.com.pnsuite.toankhoan.dataaccess.interfaces
{
    public interface ICommonService
    {
        ResponseData getById(long id);
        ResponseData create(String jsonData, long userId);
        ResponseData delete(int id, long userId);
        ResponseData getAllByTaxcode(CommonRequest request);
        ResponseData getTypeByCommonId(int id);
        ResponseData getTypeByCode(string code);
        ResponseData getClientFileNeedUpdate(int companyId, long userId, string json);
        ResponseData removeClientFileNeedUpdate(int companyId, long userId, string json);
    }
}
