using System;
using System.Collections.Generic;
using System.Text;
using vn.com.pnsuite.common.models;

namespace vn.com.pnsuite.toankhoan.dataaccess.Interfaces
{
    public interface IBankService
    {
        ResponseData DeletePaymentById(long companyId, long userId, string json);
        ResponseData DeleteReceiveById(long companyId, long userId, string json);
        ResponseData GetPaymentById(long companyId, long userId, string json);
        ResponseData GetPaymentList(long companyId, long userId, string json);
        ResponseData GetReceiveById(long companyId, long userId, string json);
        ResponseData GetReceiveList(long companyId, long userId, string json);
        ResponseData UpdatePayment(long companyId, long userId, string json);
        ResponseData UpdateReceive(long companyId, long userId, string json);
    }
}
