using System;
using System.Collections.Generic;
using System.Text;
using vn.com.pnsuite.common.models;

namespace vn.com.pnsuite.toankhoan.dataaccess.Interfaces
{
    public interface IWarehouseService
    {
        ResponseData DeleteWarehouseDeliveryById(long companyId, long userId, string json);
        ResponseData DeleteWarehouseInputById(long companyId, long userId, string json);
        ResponseData DeleteWarehouseOutputById(long companyId, long userId, string json);
        ResponseData GetWarehouseDeliveryById(long companyId, long userId, string json);
        ResponseData GetWarehouseDeliveryList(long companyId, long userId, string json);
        ResponseData GetWarehouseInputById(long companyId, long userId, string json);
        ResponseData GetWarehouseInputList(long companyId, long userId, string json);
        ResponseData GetWarehouseOutputById(long companyId, long userId, string json);
        ResponseData GetWarehouseOutputList(long companyId, long userId, string json);
        ResponseData UpdateWarehouseDelivery(long companyId, long userId, string json);
        ResponseData UpdateWarehouseInput(long companyId, long userId, string json);
        ResponseData UpdateWarehouseOutput(long companyId, long userId, string json);
    }
}
