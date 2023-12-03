using System;
using System.Collections.Generic;
using System.Text;

namespace vn.com.pnsuite.toankhoan.models.function
{
    public class FunctionModel
    {
        public int Id { get; set; }
        public string FunctionCode { get; set; }
        public string FunctionText { get; set; }
        public Boolean Accessibly { get; set; }
        public int? ParentId { get; set; }
        public string ParentCode { get; set; }
    }
}
