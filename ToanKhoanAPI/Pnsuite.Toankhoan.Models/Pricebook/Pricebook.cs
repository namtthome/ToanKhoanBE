using System;
using System.Collections.Generic;

namespace vn.com.pnsuite.toankhoan.models.Pricebook
{
    public class Pricebook
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long CreatedBy { get; set; }
        public long ModifiedBy { get; set; }
        public bool IsDeleted { get; set; }
        public int CompanyId { get; set; }
        public string Description { get; set; }
        public List<PricebookDetail> PricebookDetails { get; set; }
    }
}


