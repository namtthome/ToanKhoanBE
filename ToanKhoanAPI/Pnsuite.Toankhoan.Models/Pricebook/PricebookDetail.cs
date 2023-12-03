

using System;

namespace vn.com.pnsuite.toankhoan.models.Pricebook
{
    public class PricebookDetail
    {
        public long Id { get; set; }
        public long ProductId { get; set; }
        public float Price { get; set; }
        public long PricebookId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long CreatedBy { get; set; }
        public long ModifiedBy { get; set; }
        public long CompanyId { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public string ProductShortName { get; set; }
    }
}
