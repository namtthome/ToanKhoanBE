using System;
using System.Collections.Generic;
using System.Text;

namespace vn.com.pnsuite.toankhoan.models.user
{
    public class UserChangePasswordModel
    {
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string RetypeNewPassword { get; set; }
    }
}
