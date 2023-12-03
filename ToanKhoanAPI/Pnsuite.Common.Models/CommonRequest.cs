using System;
using System.Collections.Generic;
using System.Text.Json;

namespace vn.com.pnsuite.common.models
{
    public class CommonRequest
    {
        public String Action { get; set; }
        public String Taxcode { get; set; }
        public String Company { get; set; }
        public String Address { get; set; }
        public List<CommonRequestValue> Values { get; set; }
        public CommonRequestValue GetRequestValue(String Code)
        {
            return Values == null ? null : Values.Find(e => e.Code == Code);
        }

        public string JsonValue
        {
            get
            {
                return Values == null ? "[]" : JsonSerializer.Serialize(Values);
            }
        }
    }
}
