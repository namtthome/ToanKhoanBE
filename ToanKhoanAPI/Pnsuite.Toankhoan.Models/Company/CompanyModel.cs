namespace vn.com.pnsuite.hrm.models.company
{
    public class CompanyModel
    {
        public int Id { get; set; }
        public string Taxcode { get; set; }
        public string CompanyName { get; set; }
        public string CompanyTel { get; set; }
        public string BusinessLine { get; set; }
        public string CompanyAddress { get; set; }
        public string Ward { get; set; }
        public string District { get; set; }
        public string Province { get; set; }
        public string Representative { get; set; }
        public string RepresentativePosition { get; set; }
        public string RepresentativeTel { get; set; }
        public string RepresentativeAddress { get; set; }
        public string TaxAuthorities { get; set; }
        public int TaxAuthorityId { get; set; }
    }
}
