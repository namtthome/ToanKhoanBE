using System;
using System.Collections.Generic;
using System.Text;

namespace vn.com.pnsuite.toankhoan.models.function
{
    public class SaveUserFunctionModel
    {
        public long UserId { get; set; }
        public List<FunctionModel> Functions { get; set; }
        public List<FunctionModel> Departments { get; set; }
        public List<FunctionModel> Positions { get; set; }
    }
}
