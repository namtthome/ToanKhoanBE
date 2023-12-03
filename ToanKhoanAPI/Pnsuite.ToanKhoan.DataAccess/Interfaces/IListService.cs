using System.Threading.Tasks;
using vn.com.pnsuite.common.models;

namespace vn.com.pnsuite.toankhoan.dataaccess.Interfaces.Categories
{
    public interface IListService
    {

        #region BankAccount
        Task<ResponseData> GetDetailBankAccount(long companyId, long userId, string json);
        Task<ResponseData> GetAllBankAccountListBySearch(long companyId, long userId, string json);
        Task<ResponseData> CreateOrUpdateBankAccountAsync(long companyId, long userId, string json);
        Task<ResponseData> DeleteBankAccountAsync(long companyId, long userId, string json);
        #endregion

        #region Attribute
        Task<ResponseData> GetAllAttributeBySearch(long companyId, long userId, string json);
        #endregion

        #region ImportExportType
        Task<ResponseData> GetDetailImportExportType(long companyId, long userId, string json);
        Task<ResponseData> GetAllImportExportTypeBySearch(long companyId, long userId, string json);
        Task<ResponseData> CreateOrUpdateImportExportTypeAsync(long companyId, long userId, string json);
        Task<ResponseData> DeleteImportExportTypeAsync(long companyId, long userId, string json);

        #endregion

        #region PaymentMethod

        Task<ResponseData> GetDetailPaymentMethod(long companyId, long userId, string json);
        Task<ResponseData> GetAllPaymentMethodBySearch(long companyId, long userId, string json);
        Task<ResponseData> CreateOrUpdatePaymentMethodAsync(long companyId, long userId, string json);
        Task<ResponseData> DeletePaymentMethodAsync(long companyId, long userId, string json);

        #endregion

        #region Partner
        Task<ResponseData> GetAllPartnerBySearch(long companyId, long userId, string searchType, string json);
        Task<ResponseData> GetDetailPartner(long companyId, long userId, string json);
        Task<ResponseData> CreateOrUpdatePartnerAsync(long companyId, long userId, string json);
        Task<ResponseData> DeletePartnerAsync(long companyId, long userId, string json);
        #endregion

        #region Warehouse
        Task<ResponseData> GetDetailWasehouse(long companyId, long userId, string json);
        Task<ResponseData> GetAllWasehouseListBySearch(long companyId, long userId, string json);
        Task<ResponseData> CreateOrUpdateWarehouseAsync(long companyId, long userId, string json);
        Task<ResponseData> DeleteWarehouseAsync(long companyId, long userId, string json);
        #endregion

        #region Unit
        Task<ResponseData> GetAllUnitListBySearch(long companyId, long userId, string json);
        Task<ResponseData> GetDetailUnit(long companyId, long userId, string json);
        Task<ResponseData> DeleteUnitAsync(long companyId, long userId, string json);
        Task<ResponseData> CreateOrUpdateUnitAsync(long companyId, long userId, string json);
        ResponseData GetUnitByProduct(long companyId, long userId, string json);
        #endregion

        #region TransactionType
        Task<ResponseData> GetAllTransactionTypeBySearch(long companyId, long userId, string json);
        Task<ResponseData> GetAllImportTypeBySearch(long companyId, long userId, string json);
        Task<ResponseData> GetAllExportTypeBySearch(long companyId, long userId, string json);
        Task<ResponseData> GetAllCashTransactionTypeBySearch(long companyId, long userId, string json);
        Task<ResponseData> GetAllReceiveTransactionTypeBySearch(long companyId, long userId, string json);
        Task<ResponseData> GetAllPaymentTransactionTypeBySearch(long companyId, long userId, string json);
        Task<ResponseData> GetAllTaxBySearch(long companyId, long userId, string json);
        Task<ResponseData> GetAllTaxTypeBySearch(long companyId, long userId, string json);
        Task<ResponseData> GetDetailPaymentTearm(long companyId, long userId, string json);
        Task<ResponseData> GetAllPaymentTearmListBySearch(long companyId, long userId, string json);
        Task<ResponseData> CreateOrUpdatePaymentTearmAsync(long companyId, long userId, string json);
        Task<ResponseData> DeletePaymentTearmAsync(long companyId, long userId, string json);
        Task<ResponseData> GetDetailTransactionType(long companyId, long userId, string json);

        #endregion

        #region Product Category
        Task<ResponseData> CreateOrUpdateProductCategoryAsync(long companyId, long userId, string json);
        Task<ResponseData> DeleteProductCategoryAsync(long companyId, long userId, string json);
        Task<ResponseData> GetAllProductCategoryBySearch(long companyId, long userId, string json);
        Task<ResponseData> GetDetailProductCategory(long companyId, long userId, string json);
        #endregion
    }
}
