namespace vn.com.pnsuite.toankhoan.models.Products.Domains
{
    public class ProductAttribute
    {
        public long Id { get; set; }
        public long ProductId { get; set; }
        public int AttributeId { get; set; }
        public string AttributeKey { get; set; }
        public string AttributeValue { get; set; }
        public int CompanyId { get; set; }
    }
}
