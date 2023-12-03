using System;
using System.Collections.Generic;
using System.Text;
using vn.com.pnsuite.common.models;

namespace vn.com.pnsuite.toankhoan.dataaccess.Interfaces
{
    public interface IReportService
    {
        ResponseData GetInvoiceReportById(long companyId, long userId, string json);
        ResponseData GetDailyReportP1ById(long companyId, long userId, string json);
        ResponseData GetDailyReportP2ById(long companyId, long userId, string json);
        ResponseData GetDailyReportP3ById(long companyId, long userId, string json);
        ResponseData GetDailyReportListPurchase(long companyId, long userId, string json);
        ResponseData GetDailyReportListInvoice(long companyId, long userId, string json);
        ResponseData GetInventoryReportBalance(long companyId, long userId, string json);
        ResponseData GetReceivableReport(long companyId, long userId, string json);
        ResponseData GetPayableReport(long companyId, long userId, string json);
        ResponseData GetBankReport(long companyId, long userId, string json);
        ResponseData GetCashReport(long companyId, long userId, string json);
        ResponseData GetReceivableDetailReport(long companyId, long userId, string json);
        ResponseData GetPayableDetailReport(long companyId, long userId, string json);
        ResponseData GetReportData(long companyId, long userId, string code, string json);
        ResponseData GetQuotationReportData(long companyId, long userId, string json);
    }
}
