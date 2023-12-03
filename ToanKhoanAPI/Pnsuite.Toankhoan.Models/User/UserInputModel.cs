using System;
using System.Collections.Generic;
using System.Text;

namespace vn.com.pnsuite.toankhoan.models.user
{
    public class UserInputModel
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public Boolean Locked { get; set; }
        public int CompanyId { get; set; }
        public string Company { get; set; }
        public string CompanyTaxcode { get; set; }
    }
}
