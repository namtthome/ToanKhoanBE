using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace vn.com.pnsuite.toankhoan.models.authentication
{
    public class AuthenticateRequest
    {
        [Required]
        public string Taxcode { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
