namespace vn.com.pnsuite.toankhoan.models.Products.Domains
{
    public class ProductHasExtraInfo
    {
        public long Id { get; set; }
        public long ProductId { get; set; }
        public long InputExtraInfoId { get; set; }
        public string InputExtraInfoName { get; set; }
        public string InputExtraInfoCode { get; set; }
        public long CompanyId { get; set; }
    }
}
