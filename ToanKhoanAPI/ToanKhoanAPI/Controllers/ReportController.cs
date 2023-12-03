using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using vn.com.pnsuite.common;
using vn.com.pnsuite.common.models;
using vn.com.pnsuite.toankhoan.Controllers;
using vn.com.pnsuite.toankhoan.dataaccess.interfaces;
using vn.com.pnsuite.toankhoan.dataaccess.Interfaces;
using vn.com.pnsuite.toankhoan.Helpers;

namespace Pnsuite.ToanKhoan.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ReportController : BaseApiController
    {
        private readonly IReportService _reportService;

        public ReportController(IUserService userService, IReportService reportService) : base(userService)
        {
            _reportService = reportService;
        }
        [HttpPost("get-invoice-by-id")]
        public async Task<IActionResult> GetReceiveById([FromBody] CommonRequest request)
        {
            var response = _reportService.GetInvoiceReportById(this.CurrentUser.CompanyId, this.CurrentUser.Id, request.JsonValue);
            return Ok(response);
        }
        [HttpPost("get-daily-report_p1")]
        public async Task<IActionResult> GetDailyReportP1([FromBody] CommonRequest request)
        {
            var response = _reportService.GetDailyReportP1ById(this.CurrentUser.CompanyId, this.CurrentUser.Id, request.JsonValue);
            return Ok(response);
        }
        [HttpPost("get-daily-report_p2")]
        public async Task<IActionResult> GetDailyReportP2([FromBody] CommonRequest request)
        {
            var response = _reportService.GetDailyReportP2ById(this.CurrentUser.CompanyId, this.CurrentUser.Id, request.JsonValue);
            return Ok(response);
        }
        [HttpPost("get-daily-report_p3")]
        public async Task<IActionResult> GetDailyReportP3([FromBody] CommonRequest request)
        {
            var response = _reportService.GetDailyReportP3ById(this.CurrentUser.CompanyId, this.CurrentUser.Id, request.JsonValue);
            return Ok(response);
        }
        [HttpPost("get-daily-report-list-purchase")]
        public async Task<IActionResult> GetDailyReportListPurchase([FromBody] CommonRequest request)
        {
            var response = _reportService.GetDailyReportListPurchase(this.CurrentUser.CompanyId, this.CurrentUser.Id, request.JsonValue);
            return Ok(response);
        }
        [HttpPost("get-daily-report-list-invoice")]
        public async Task<IActionResult> GetDailyReportListInvoice([FromBody] CommonRequest request)
        {
            var response = _reportService.GetDailyReportListInvoice(this.CurrentUser.CompanyId, this.CurrentUser.Id, request.JsonValue);
            return Ok(response);
        }
        [HttpPost("get-inventory-report-balance")]
        public async Task<IActionResult> GetInventoryReportBalance([FromBody] CommonRequest request)
        {
            var response = _reportService.GetInventoryReportBalance(this.CurrentUser.CompanyId, this.CurrentUser.Id, request.JsonValue);
            return Ok(response);
        }
        [HttpPost("get-payable-report")]
        public async Task<IActionResult> GetPayableReport([FromBody] CommonRequest request)
        {
            var response = _reportService.GetPayableReport(this.CurrentUser.CompanyId, this.CurrentUser.Id, request.JsonValue);
            return Ok(response);
        }
        [HttpPost("get-receivable-report")]
        public async Task<IActionResult> GetReceivableReport([FromBody] CommonRequest request)
        {
            var response = _reportService.GetReceivableReport(this.CurrentUser.CompanyId, this.CurrentUser.Id, request.JsonValue);
            return Ok(response);
        }
        [HttpPost("get-bank-report")]
        public async Task<IActionResult> GetBankReport([FromBody] CommonRequest request)
        {
            var response = _reportService.GetBankReport(this.CurrentUser.CompanyId, this.CurrentUser.Id, request.JsonValue);
            return Ok(response);
        }
        [HttpPost("get-cash-report")]
        public async Task<IActionResult> GetCashReport([FromBody] CommonRequest request)
        {
            var response = _reportService.GetCashReport(this.CurrentUser.CompanyId, this.CurrentUser.Id, request.JsonValue);
            return Ok(response);
        }
        [HttpPost("get-receivable-detail")]
        public async Task<IActionResult> GetReceivableDetailReport([FromBody] CommonRequest request)
        {
            var response = _reportService.GetReceivableDetailReport(this.CurrentUser.CompanyId, this.CurrentUser.Id, request.JsonValue);
            return Ok(response);
        }
        [HttpPost("get-payable-detail")]
        public async Task<IActionResult> GetPayableDetailReport([FromBody] CommonRequest request)
        {
            var response = _reportService.GetPayableDetailReport(this.CurrentUser.CompanyId, this.CurrentUser.Id, request.JsonValue);
            return Ok(response);
        }
        [HttpPost("export-to-excel")]
        public async Task<IActionResult> ExportJsonToExcel([FromBody] dynamic request)
        {
            //Require data in json
            //Tag template: để lấy file mẫu
            //Tag param dạng jsonarray parameters: [{'columnName': 'A', 'row': 2, 'value'}]
            //Tag data dạng jsonarray
            var jsonData = (JObject)JsonConvert.DeserializeObject(Convert.ToString(request)); 
            var template = Path.Combine(AppContext.BaseDirectory, "Template\\" + jsonData["template"].ToString() + ".xlsx"); 
            string paramData = jsonData["parameters"].ToString();
            var parameter = JsonConvert.DeserializeObject<List<ExcelParamer>>(Convert.ToString(paramData));
            var startRow = Convert.ToInt32(jsonData["startRow"].ToString());

            string jsonString = Convert.ToString(jsonData["data"].ToString());

            var file = ExcelHelper.ExportData(jsonString, template, startRow, parameter);
            String fileName = String.Format("{0}-{1}.xlsx", jsonData["template"].ToString(), DateTime.Now.ToString("yyyyMMddHHmmss"));
            return File(file.ToArray(), "application/octet-stream", fileName);
        }
        [HttpPost("get-report-data")]
        public async Task<IActionResult> GetReportData([FromBody] CommonRequest request)
        {
            var response = _reportService.GetReportData(this.CurrentUser.CompanyId, this.CurrentUser.Id, request.GetRequestValue("ReportCode").Value.ToString(), request.JsonValue);
            return Ok(response);
        }
        [HttpPost("get-quotation-report")]
        public async Task<IActionResult> GetQuotationReport([FromBody] CommonRequest request)
        {
            var response = _reportService.GetQuotationReportData(this.CurrentUser.CompanyId, this.CurrentUser.Id, request.JsonValue);
            return Ok(response);
        }
    }
}
