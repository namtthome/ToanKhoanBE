using System;

namespace vn.com.pnsuite.common.models
{
    public class ResponseData
    {
        public string ActionResult { get; set; }
        public Object ActionData { get; set; }
        public ErrorDataModel ErrorData { get; set; }
    }
}
