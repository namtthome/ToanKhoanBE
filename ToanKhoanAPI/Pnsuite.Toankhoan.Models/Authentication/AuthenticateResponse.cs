using System;
using System.Collections.Generic;
using vn.com.pnsuite.common.models;
using vn.com.pnsuite.toankhoan.models.function;
using vn.com.pnsuite.toankhoan.models.user;

namespace vn.com.pnsuite.toankhoan.models.authentication
{
    public class AuthenticateResponse
    {
        public string ActionResult { get; set; }
        public AuthenticateData ActionData { get; set; }
        public ErrorDataModel ErrorData { get; set; }
        public AuthenticateResponse()
        {

        }
        public AuthenticateResponse(string result, UserModel user, string token, string refreshToken, List<FunctionModel> functions)
        {
            ActionResult = result;
            if (user != null)
            {
                ActionData = new AuthenticateData()
                {
                    Token = token,
                    RefreshToken = refreshToken,
                    UserInfo = new UserInfo()
                    {
                        UserName = user == null ? "" : user.Username,
                        UserDescription = user == null ? "" : user.LastName + " " + user.FirstName,
                        Company = user == null ? "" : user.Company,
                        CompanyTaxcode = user == null ? "" : user.CompanyTaxcode
                    },
                    AccessData = new AccessData() { FunctionList = functions },
                    ExtraData = user
                };
            }
            ErrorData = null;
        }
    }

    public class AuthenticateData
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public UserInfo UserInfo { get; set; }
        public AccessData AccessData { get; set; }
        public Object ExtraData { get; set; }
    }

    public class UserInfo
    {
        public string UserName { get; set; }
        public string UserDescription { get; set; }
        public string CompanyTaxcode { get; set; }
        public string Company { get; set; }
    }
    public class AccessData
    {
        public List<FunctionModel> FunctionList { get; set; }
    }
}
