namespace vn.com.pnsuite.toankhoan.models.Products.Domains
{
    public class ProductInventory
    {
        public long ProductId { get; set; }
        public int WareHouseId { get; set; }
        public int CompanyId { get; set; }
        public string WareHouseName { get; set; }
        public float StockValue { get; set; }
        public bool IsDefaultWarehouse { get; set; }
    }
}
