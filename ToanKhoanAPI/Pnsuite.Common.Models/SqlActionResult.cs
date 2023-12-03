using System;
using System.Collections.Generic;
using System.Text;

namespace vn.com.pnsuite.common.models
{
    public class SqlActionResult
    {
        public String ActionCode { get; set; }
        public String ActionMessage { get; set; }
        public Boolean HasData { get; set; }
        public Object ExtendData { get; set; }
    }
}
