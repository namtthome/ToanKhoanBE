using System;
using System.Collections.Generic;
using System.Text;

namespace vn.com.pnsuite.common.models
{
    public class ExcelValue
    {
        public int Row { get; set; }
        public string Column { get; set; }
        public string CellAddress { get; set; }
        public string CellValue { get; set; }
    }
}
