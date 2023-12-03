using System;
using System.Collections.Generic;
using System.Text;
using vn.com.pnsuite.common.models;
using vn.com.pnsuite.toankhoan.models.authentication;
using vn.com.pnsuite.toankhoan.models.user;

namespace vn.com.pnsuite.toankhoan.dataaccess.interfaces
{
    public interface IUserService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model, string ipAddress);
        AuthenticateResponse RefreshToken(string token, string ipAddress);
        bool RevokeToken(string token, string ipAddress);
        ResponseData GetActive(long id, string token);
        ResponseData GetAllByTaxcode(long id, string taxcode, string search);
        ResponseData GetById(long id);
        ResponseData GetByToken(String token);
        ResponseData Create(UserModel model, long userId);
        ResponseData Lock(long id, long userId);
        ResponseData Reset(UserInputModel model, long userId);
        ResponseData UnLock(long id, long userId);
        ResponseData Delete(long id, long userId);
        ResponseData ChangePassword(UserChangePasswordModel model, long userId);
        bool IsFunctionRight(string DoNotCheckPermission, string functionCode, long userId);
    }
}
