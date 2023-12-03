using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Threading.Tasks;
using vn.com.pnsuite.common.models;
using vn.com.pnsuite.toankhoan.Controllers;
using vn.com.pnsuite.toankhoan.dataaccess.interfaces;
using vn.com.pnsuite.toankhoan.dataaccess.Interfaces.Categories;
using vn.com.pnsuite.toankhoan.Helpers;

namespace Pnsuite.ToanKhoan.Controllers.Categories
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ListController : BaseApiController
    {
        private readonly IListService _listService;

        public ListController(IUserService userService, IListService listService) : base(userService)
        {
            _listService = listService;
        }

        #region Bank account
        [HttpPost("bankaccount-get-all")]
        public async Task<IActionResult> GetAllBankAccountListBySearch(CommonRequest request)
        {
            var response = await _listService.GetAllBankAccountListBySearch(CurrentUser.CompanyId, CurrentUser.Id, request.JsonValue);
            return Ok(response);
        }

        [HttpPost("bankaccount-detail")]
        public async Task<IActionResult> GetDetailBankAccount(CommonRequest request)
        {
            var response = await _listService.GetDetailBankAccount(CurrentUser.CompanyId, CurrentUser.Id, request.JsonValue);
            return Ok(response);
        }

        [HttpPost("bankaccount-create")]
        public async Task<IActionResult> CreateBankAccountAsync([FromBody] dynamic request)
        {
            var response = await _listService.CreateOrUpdateBankAccountAsync(CurrentUser.CompanyId, CurrentUser.Id, JsonSerializer.Serialize(request));
            return Ok(response);
        }

        [HttpPost("bankaccount-update")]
        public async Task<IActionResult> UpdateBankAccountAsync([FromBody] dynamic request)
        {
            var response = await _listService.CreateOrUpdateBankAccountAsync(CurrentUser.CompanyId, CurrentUser.Id, JsonSerializer.Serialize(request));
            return Ok(response);
        }

        [HttpPost("bankaccount-delete")]
        public async Task<IActionResult> DeleteBankAccountAsync(CommonRequest request)
        {
            var response = await _listService.DeleteBankAccountAsync(CurrentUser.CompanyId, CurrentUser.Id, request.JsonValue);
            return Ok(response);
        }

        #endregion

        #region PaymentTearm
        [HttpPost("payment-tearm-get-all")]
        public async Task<IActionResult> GetAllPaymentTearmListBySearch(CommonRequest request)
        {
            var response = await _listService.GetAllPaymentTearmListBySearch(CurrentUser.CompanyId, CurrentUser.Id, request.JsonValue);
            return Ok(response);
        }

        [HttpPost("payment-tearm-detail")]
        public async Task<IActionResult> GetDetailPaymentTearm(CommonRequest request)
        {
            var response = await _listService.GetDetailPaymentTearm(CurrentUser.CompanyId, CurrentUser.Id, request.JsonValue);
            return Ok(response);
        }

        [HttpPost("payment-tearm-create")]
        public async Task<IActionResult> CreatePaymentTearmAsync([FromBody] dynamic request)
        {
            var response = await _listService.CreateOrUpdatePaymentTearmAsync(CurrentUser.CompanyId, CurrentUser.Id, JsonSerializer.Serialize(request));
            return Ok(response);
        }

        [HttpPost("payment-tearm-update")]
        public async Task<IActionResult> UpdatPaymentTearmAsync([FromBody] dynamic request)
        {
            var response = await _listService.CreateOrUpdatePaymentTearmAsync(CurrentUser.CompanyId, CurrentUser.Id, JsonSerializer.Serialize(request));
            return Ok(response);
        }

        [HttpPost("payment-tearm-delete")]
        public async Task<IActionResult> DeletePaymentTearmAsync(CommonRequest request)
        {
            var response = await _listService.DeletePaymentTearmAsync(CurrentUser.CompanyId, CurrentUser.Id, request.JsonValue);
            return Ok(response);
        }

        #endregion

        #region Attribute
        [HttpPost("attribute-get-all")]
        public async Task<IActionResult> GetAllAttributeListBySearch(CommonRequest request)
        {
            var response = await _listService.GetAllAttributeBySearch(CurrentUser.CompanyId, CurrentUser.Id, request.JsonValue);
            return Ok(response);
        }
        #endregion

        #region Partner
        [HttpPost("partner-get-all")]
        public async Task<IActionResult> GetAllPartnerBySearch(CommonRequest request)
        {
            var response = await _listService.GetAllPartnerBySearch(this.CurrentUser.CompanyId, this.CurrentUser.Id, "", request.Values == null ? "[]" : JsonSerializer.Serialize(request.Values));
            return Ok(response);
        }
        [HttpPost("supplier-get-all")]
        public async Task<IActionResult> GetAllSupplierBySearch(CommonRequest request)
        {
            var response = await _listService.GetAllPartnerBySearch(this.CurrentUser.CompanyId, this.CurrentUser.Id, "SUP", request.Values == null ? "[]" : JsonSerializer.Serialize(request.Values));
            return Ok(response);
        }

        [HttpPost("customer-get-all")]
        public async Task<IActionResult> GetAllCustomerBySearch(CommonRequest request)
        {
            var response = await _listService.GetAllPartnerBySearch(this.CurrentUser.CompanyId, this.CurrentUser.Id, "CUS", request.Values == null ? "[]" : JsonSerializer.Serialize(request.Values));
            return Ok(response);
        }
        [HttpPost("employee-get-all")]
        public async Task<IActionResult> GetAllEmployeeBySearch(CommonRequest request)
        {
            var response = await _listService.GetAllPartnerBySearch(this.CurrentUser.CompanyId, this.CurrentUser.Id, "EMP", request.Values == null ? "[]" : JsonSerializer.Serialize(request.Values));
            return Ok(response);
        }

        [HttpPost("partner-detail")]
        public async Task<IActionResult> GetDetailPartner(CommonRequest request)
        {
            var response = await _listService.GetDetailPartner(CurrentUser.CompanyId, CurrentUser.Id, request.JsonValue);
            return Ok(response);
        }

        [HttpPost("partner-create")]
        public async Task<IActionResult> CreatePartnerAsync([FromBody] dynamic request)
        {
            var response = await _listService.CreateOrUpdatePartnerAsync(CurrentUser.CompanyId, CurrentUser.Id, JsonSerializer.Serialize(request));
            return Ok(response);
        }

        [HttpPost("partner-update")]
        public async Task<IActionResult> UpdatePartnerAsync([FromBody] dynamic request)
        {
            var response = await _listService.CreateOrUpdatePartnerAsync(CurrentUser.CompanyId, CurrentUser.Id, JsonSerializer.Serialize(request));
            return Ok(response);
        }

        [HttpPost("partner-delete")]
        public async Task<IActionResult> DeletePartnerAsync(CommonRequest request)
        {
            var response = await _listService.DeletePartnerAsync(CurrentUser.CompanyId, CurrentUser.Id, request.JsonValue);
            return Ok(response);
        }

        #endregion

        #region Unit
        [HttpPost("unit-get-all")]
        public async Task<IActionResult> GetAllUnitListBySearch(CommonRequest request)
        {
            var response = await _listService.GetAllUnitListBySearch(CurrentUser.CompanyId, CurrentUser.Id, request.JsonValue);
            return Ok(response);
        }
        [HttpPost("unit-detail")]
        public async Task<IActionResult> GetDetailUnit(CommonRequest request)
        {
            var response = await _listService.GetDetailUnit(CurrentUser.CompanyId, CurrentUser.Id, request.JsonValue);
            return Ok(response);
        }
        [HttpPost("unit-create")]
        public async Task<IActionResult> CreateUnitAsync([FromBody] dynamic request)
        {
            var response = await _listService.CreateOrUpdateUnitAsync(CurrentUser.CompanyId, CurrentUser.Id, JsonSerializer.Serialize(request));
            return Ok(response);
        }
        [HttpPost("unit-update")]
        public async Task<IActionResult> UpdateUnitAsync([FromBody] dynamic request)
        {
            var response = await _listService.CreateOrUpdateUnitAsync(CurrentUser.CompanyId, CurrentUser.Id, JsonSerializer.Serialize(request));
            return Ok(response);
        }
        [HttpPost("unit-delete")]
        public async Task<IActionResult> DeleteUnitAsync(CommonRequest request)
        {
            var response = await _listService.DeleteUnitAsync(CurrentUser.CompanyId, CurrentUser.Id, request.JsonValue);
            return Ok(response);
        }
        [HttpPost("unit-get-by-product")]
        public async Task<IActionResult> GetUnitByProduct([FromBody] CommonRequest request)
        {
            var response = _listService.GetUnitByProduct(CurrentUser.CompanyId, CurrentUser.Id, request.Values == null ? "[]" : JsonSerializer.Serialize(request.Values));
            return Ok(response);
        }
        #endregion

        #region Warehouse
        [HttpPost("warehouse-get-all")]
        public async Task<IActionResult> GetAllWasehouseListBySearch(CommonRequest request)
        {
            var response = await _listService.GetAllWasehouseListBySearch(CurrentUser.CompanyId, CurrentUser.Id, request.JsonValue);
            return Ok(response);
        }

        [HttpPost("warehouse-detail")]
        public async Task<IActionResult> GetDetailWasehouse(CommonRequest request)
        {
            var response = await _listService.GetDetailWasehouse(CurrentUser.CompanyId, CurrentUser.Id, request.JsonValue);
            return Ok(response);
        }

        [HttpPost("warehouse-create")]
        public async Task<IActionResult> CreateWarehouseAsync([FromBody] dynamic request)
        {
            var response = await _listService.CreateOrUpdateWarehouseAsync(CurrentUser.CompanyId, CurrentUser.Id, JsonSerializer.Serialize(request));
            return Ok(response);
        }

        [HttpPost("warehouse-update")]
        public async Task<IActionResult> UpdateWarehouseAsync([FromBody] dynamic request)
        {
            var response = await _listService.CreateOrUpdateWarehouseAsync(CurrentUser.CompanyId, CurrentUser.Id, JsonSerializer.Serialize(request));
            return Ok(response);
        }

        [HttpPost("warehouse-delete")]
        public async Task<IActionResult> DeleteWarehouseAsync(CommonRequest request)
        {
            var response = await _listService.DeleteWarehouseAsync(CurrentUser.CompanyId, CurrentUser.Id, request.JsonValue);
            return Ok(response);
        }
        #endregion

        #region Importexporttype
        [HttpPost("import-type-get-all")]
        public async Task<IActionResult> GetAllImportTypeBySearch(CommonRequest request)
        {
            var response = await _listService.GetAllImportTypeBySearch(CurrentUser.CompanyId, CurrentUser.Id, request.JsonValue);
            return Ok(response);
        }
        [HttpPost("export-type-get-all")]
        public async Task<IActionResult> GetAllExportTypeBySearch(CommonRequest request)
        {
            var response = await _listService.GetAllExportTypeBySearch(CurrentUser.CompanyId, CurrentUser.Id, request.JsonValue);
            return Ok(response);
        }
        [HttpPost("importexporttype-get-all")]
        public async Task<IActionResult> GetAllImportExportTypeBySearch(CommonRequest request)
        {
            var response = await _listService.GetAllImportExportTypeBySearch(CurrentUser.CompanyId, CurrentUser.Id, request.JsonValue);
            return Ok(response);
        }


        [HttpPost("importexporttype-detail")]
        public async Task<IActionResult> GetDetailImportExportType(CommonRequest request)
        {
            var response = await _listService.GetDetailImportExportType(CurrentUser.CompanyId, CurrentUser.Id, request.JsonValue);
            return Ok(response);
        }


        [HttpPost("importexporttype-create")]
        public async Task<IActionResult> CreateImportExportTypeAsync([FromBody] dynamic request)
        {
            var response = await _listService.CreateOrUpdateImportExportTypeAsync(CurrentUser.CompanyId, CurrentUser.Id, JsonSerializer.Serialize(request));
            return Ok(response);
        }
        [HttpPost("importexporttype-update")]
        public async Task<IActionResult> UpdateImportExportTypeAsync([FromBody] dynamic request)
        {
            var response = await _listService.CreateOrUpdateImportExportTypeAsync(CurrentUser.CompanyId, CurrentUser.Id, JsonSerializer.Serialize(request));
            return Ok(response);
        }
        [HttpPost("importexporttype-delete")]
        public async Task<IActionResult> DeleteImportExportTypeAsync(CommonRequest request)
        {
            var response = await _listService.DeleteImportExportTypeAsync(CurrentUser.CompanyId, CurrentUser.Id, request.JsonValue);
            return Ok(response);
        }

        #endregion

        #region Paymentmethod

        [HttpPost("payment-method-get-all")]
        public async Task<IActionResult> GetAllPaymentMethodBySearch(CommonRequest request)
        {
            var response = await _listService.GetAllPaymentMethodBySearch(CurrentUser.CompanyId, CurrentUser.Id, request.JsonValue);
            return Ok(response);
        }


        [HttpPost("payment-method-detail")]
        public async Task<IActionResult> GetDetailPaymentMethod(CommonRequest request)
        {
            var response = await _listService.GetDetailPaymentMethod(CurrentUser.CompanyId, CurrentUser.Id, request.JsonValue);
            return Ok(response);
        }


        [HttpPost("payment-method-create")]
        public async Task<IActionResult> CreatePaymentMethodAsync([FromBody] dynamic request)
        {
            var response = await _listService.CreateOrUpdatePaymentMethodAsync(CurrentUser.CompanyId, CurrentUser.Id, JsonSerializer.Serialize(request));
            return Ok(response);
        }
        [HttpPost("payment-method-update")]
        public async Task<IActionResult> UpdatePaymentMethodAsync([FromBody] dynamic request)
        {
            var response = await _listService.CreateOrUpdatePaymentMethodAsync(CurrentUser.CompanyId, CurrentUser.Id, JsonSerializer.Serialize(request));
            return Ok(response);
        }
        [HttpPost("payment-method-delete")]
        public async Task<IActionResult> DeletePaymentMethodAsync(CommonRequest request)
        {
            var response = await _listService.DeletePaymentMethodAsync(CurrentUser.CompanyId, CurrentUser.Id, request.JsonValue);
            return Ok(response);
        }
        #endregion

        #region TransactionType
        [HttpPost("transaction-type-get-all")]
        public async Task<IActionResult> GetAllTransactionTypeBySearch(CommonRequest request)
        {
            var response = await _listService.GetAllTransactionTypeBySearch(CurrentUser.CompanyId, CurrentUser.Id, request.JsonValue);
            return Ok(response);
        }
        [HttpPost("cash-transaction-type-list")]
        public async Task<IActionResult> GetAllCashtTransactionTypeBySearch(CommonRequest request)
        {
            var response = await _listService.GetAllCashTransactionTypeBySearch(CurrentUser.CompanyId, CurrentUser.Id, request.JsonValue);
            return Ok(response);
        }
        [HttpPost("payment-transaction-type-list")]
        public async Task<IActionResult> GetAllPaymentTransactionTypeBySearch(CommonRequest request)
        {
            var response = await _listService.GetAllPaymentTransactionTypeBySearch(CurrentUser.CompanyId, CurrentUser.Id, request.JsonValue);
            return Ok(response);
        }
        [HttpPost("receive-transaction-type-list")]
        public async Task<IActionResult> GetAllReceiveTransactionTypeBySearch(CommonRequest request)
        {
            var response = await _listService.GetAllReceiveTransactionTypeBySearch(CurrentUser.CompanyId, CurrentUser.Id, request.JsonValue);
            return Ok(response);
        }
        [HttpPost("inventory-transaction-type-detail")]
        public async Task<IActionResult> GetTransactionTypeById(CommonRequest request)
        {
            var response = await _listService.GetDetailTransactionType(CurrentUser.CompanyId, CurrentUser.Id, request.JsonValue);
            return Ok(response);
        }
        #endregion

        #region Tax
        [HttpPost("tax-type-get-all")]
        public async Task<IActionResult> GetAllTaxTypeBySearch(CommonRequest request)
        {
            var response = await _listService.GetAllTaxTypeBySearch(CurrentUser.CompanyId, CurrentUser.Id, request.JsonValue);
            return Ok(response);
        }
        [HttpPost("tax-get-all")]
        public async Task<IActionResult> GetAllTaxBySearch(CommonRequest request)
        {
            var response = await _listService.GetAllTaxBySearch(CurrentUser.CompanyId, CurrentUser.Id, request.JsonValue);
            return Ok(response);
        }
        #endregion

        #region Product Category
        [HttpPost("product-category-get-all")]
        public async Task<IActionResult> GetAllProductCategoryBySearch(CommonRequest request)
        {
            var response = await _listService.GetAllProductCategoryBySearch(this.CurrentUser.CompanyId, this.CurrentUser.Id, request.Values == null ? "[]" : JsonSerializer.Serialize(request.Values));
            return Ok(response);
        }
        [HttpPost("product-category-detail")]
        public async Task<IActionResult> GetDetailProductCategory(CommonRequest request)
        {
            var response = await _listService.GetDetailProductCategory(CurrentUser.CompanyId, CurrentUser.Id, request.JsonValue);
            return Ok(response);
        }

        [HttpPost("product-category-create")]
        public async Task<IActionResult> CreateProductCategoryAsync([FromBody] dynamic request)
        {
            var response = await _listService.CreateOrUpdateProductCategoryAsync(CurrentUser.CompanyId, CurrentUser.Id, JsonSerializer.Serialize(request));
            return Ok(response);
        }

        [HttpPost("product-category-update")]
        public async Task<IActionResult> UpdateProductCategoryAsync([FromBody] dynamic request)
        {
            var response = await _listService.CreateOrUpdateProductCategoryAsync(CurrentUser.CompanyId, CurrentUser.Id, JsonSerializer.Serialize(request));
            return Ok(response);
        }

        [HttpPost("product-category-delete")]
        public async Task<IActionResult> DeleteProductCategoryAsync(CommonRequest request)
        {
            var response = await _listService.DeleteProductCategoryAsync(CurrentUser.CompanyId, CurrentUser.Id, request.JsonValue);
            return Ok(response);
        }

        #endregion
    }
}
