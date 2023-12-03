using System;
using System.Collections.Generic;
using System.Text;
using vn.com.pnsuite.common.models;

namespace vn.com.pnsuite.toankhoan.dataaccess.Interfaces
{
    public interface IPurchasingService
    {
        ResponseData DeleteOrderById(long companyId, long userId, string json);
        ResponseData DeleteReturnById(long companyId, long userId, string json);
        ResponseData DeleteVoucherById(long companyId, long userId, string json);
        ResponseData GetCopyDataReturnById(long companyId, long userId, string json);
        ResponseData GetCopyVoucher(long companyId, long userId, string json);
        ResponseData GetOrderById(long companyId, long userId, string json);
        ResponseData GetOrderList(long companyId, long userId, string json);
        ResponseData GetReturnById(long companyId, long userId, string json);
        ResponseData GetReturnList(long companyId, long userId, string json);
        ResponseData GetVoucherById(long companyId, long userId, string json);
        ResponseData GetVoucherList(long companyId, long userId, string json);
        ResponseData UpdateOrder(long companyId, long userId, string json);
        ResponseData UpdateReturn(long companyId, long userId, string json);
        ResponseData UpdateVoucher(long companyId, long userId, string json);
    }
}
