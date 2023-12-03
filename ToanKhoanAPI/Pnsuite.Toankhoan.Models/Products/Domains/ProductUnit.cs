namespace vn.com.pnsuite.toankhoan.models.Products.Domains
{
    public class ProductUnit
    {
        public long ProductId { get; set; }
        public long Id { get; set; }
        public long UnitId { get; set; }
        public string UnitName { get; set; }
        public bool IsBaseUnit { get; set; }
        public bool IsDefaultSaleUnit { get; set; }
        public bool IsBazemUnit { get; set; }
        public long CompanyId { get; set; }
        public float UnitValue { get; set; }
    }
}
