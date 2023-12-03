using System;
using System.Text.Json.Serialization;

namespace vn.com.pnsuite.toankhoan.models.user
{
    public class UserModel
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        [JsonIgnore]
        public string Password { get; set; }
        [JsonIgnore]
        public string Hash { get; set; }
        public bool Locked { get; set; }
        public int CompanyId { get; set; }
        public string Company { get; set; }
        public string CompanyTaxcode { get; set; }
    }
}
