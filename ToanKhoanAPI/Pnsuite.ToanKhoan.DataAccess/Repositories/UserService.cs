using Dapper;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using vn.com.pnsuite.common;
using vn.com.pnsuite.common.dataaccess.interfaces;
using vn.com.pnsuite.common.models;
using vn.com.pnsuite.toankhoan.dataaccess.interfaces;
using vn.com.pnsuite.toankhoan.models.authentication;
using vn.com.pnsuite.toankhoan.models.function;
using vn.com.pnsuite.toankhoan.models.user;

namespace vn.com.pnsuite.toankhoan.dataaccess.repositories
{
    public class UserService : IUserService
    {
        private readonly IBaseService baseService;
        private readonly AppSettings appSettings;
        public UserService(IOptions<AppSettings> appSettings, IBaseService baseService)
        {
            this.appSettings = appSettings.Value;
            this.baseService = baseService;
        }
        public AuthenticateResponse Authenticate(AuthenticateRequest model, string ipAddress)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@UserName", dbType: DbType.String, value: model.Username, direction: ParameterDirection.Input);
                param.Add("@Taxcode", dbType: DbType.String, value: model.Taxcode, direction: ParameterDirection.Input);

                var user = baseService.GetSingle<UserModel>("sp_User_GetByName", param);

                // return null if user not found
                if (user == null) return new AuthenticateResponse() { ActionResult = ActionResultData.Failed, ErrorData = new ErrorDataModel() { ErrorCode = "ERROR_LOGIN_0001", ErrorMessage = "Sai tên người dùng hoặc mật khẩu" } };

                if (user.Password != null && user.Password != StringUtils.Encrypt(model.Password, user.Hash)) {
                    return new AuthenticateResponse() { ActionResult = ActionResultData.Failed, ErrorData = new ErrorDataModel() { ErrorCode = "ERROR_LOGIN_0001", ErrorMessage = "Sai tên người dùng hoặc mật khẩu" } };
                }

                var token = GenerateJwtToken(user);
                var refreshToken = GenerateRefreshToken(user.Id, ipAddress);
                refreshToken.Token = token;

                var tParam = new DynamicParameters();
                tParam.Add("@Id", dbType: DbType.Int64, value: 0, direction: ParameterDirection.Input);
                tParam.Add("@UserId", dbType: DbType.Int64, value: refreshToken.UserId, direction: ParameterDirection.Input);
                tParam.Add("@Token", dbType: DbType.String, value: refreshToken.Token, direction: ParameterDirection.Input);
                tParam.Add("@Expires", dbType: DbType.DateTime, value: refreshToken.Expires, direction: ParameterDirection.Input);
                tParam.Add("@Created", dbType: DbType.DateTime, value: refreshToken.Created, direction: ParameterDirection.Input);
                tParam.Add("@CreatedByIp", dbType: DbType.String, value: refreshToken.CreatedByIp, direction: ParameterDirection.Input);
                tParam.Add("@Revoked", dbType: DbType.DateTime, value: refreshToken.Revoked, direction: ParameterDirection.Input);
                tParam.Add("@RevokedByIp", dbType: DbType.String, value: refreshToken.RevokedByIp, direction: ParameterDirection.Input);
                tParam.Add("@ReplacedByToken", dbType: DbType.String, value: refreshToken.ReplacedByToken, direction: ParameterDirection.Input);
                baseService.Update("sp_UserToken_InsertOrUpdate", tParam);

                CreateLoginLog(model.Username, ipAddress, ActionResultData.Success);

                return new AuthenticateResponse(ActionResultData.Success, user, token, refreshToken.Token, new List<FunctionModel>());
                //if (user.Password == StringUtils.Encrypt(model.Password, user.Hash))
                //{
                //    // authentication successful so generate jwt token
                //    var token = generateJwtToken(user);
                //    var refreshToken = generateRefreshToken(user.Id, ipAddress);
                //    refreshToken.Token = token;

                //    var tParam = new DynamicParameters();
                //    tParam.Add("@Id", dbType: DbType.Int64, value: 0, direction: ParameterDirection.Input);
                //    tParam.Add("@UserId", dbType: DbType.Int64, value: refreshToken.UserId, direction: ParameterDirection.Input);
                //    tParam.Add("@Token", dbType: DbType.String, value: refreshToken.Token, direction: ParameterDirection.Input);
                //    tParam.Add("@Expires", dbType: DbType.DateTime, value: refreshToken.Expires, direction: ParameterDirection.Input);
                //    tParam.Add("@Created", dbType: DbType.DateTime, value: refreshToken.Created, direction: ParameterDirection.Input);
                //    tParam.Add("@CreatedByIp", dbType: DbType.String, value: refreshToken.CreatedByIp, direction: ParameterDirection.Input);
                //    tParam.Add("@Revoked", dbType: DbType.DateTime, value: refreshToken.Revoked, direction: ParameterDirection.Input);
                //    tParam.Add("@RevokedByIp", dbType: DbType.String, value: refreshToken.RevokedByIp, direction: ParameterDirection.Input);
                //    tParam.Add("@ReplacedByToken", dbType: DbType.String, value: refreshToken.ReplacedByToken, direction: ParameterDirection.Input);
                //    baseService.Update("sp_UserToken_InsertOrUpdate", tParam);

                //    CreateLoginLog(model.Username, ipAddress, ActionResultData.Success);

                //    return new AuthenticateResponse(ActionResultData.Success, user, token, refreshToken.Token, new List<FunctionModel>());
                //}
                //else
                //{
                //    CreateLoginLog(model.Username, ipAddress, ActionResultData.Failed);
                //    return new AuthenticateResponse() { ActionResult = ActionResultData.Failed, ErrorData = new ErrorDataModel() { ErrorCode = "ERROR_LOGIN_0001", ErrorMessage = "Sai tên người dùng hoặc mật khẩu" } };
                //}
            }
            catch (Exception ex)
            {
                return new AuthenticateResponse() { ActionResult = ActionResultData.Failed, ErrorData = new ErrorDataModel(ex) };
            }


        }
        public AuthenticateResponse RefreshToken(string token, string ipAddress)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@Token", dbType: DbType.String, value: token, direction: ParameterDirection.Input);

                var user = baseService.GetSingle<UserModel>("sp_User_GetByToken", param);

                // return null if no user found with token
                if (user == null) return null;

                param = new DynamicParameters();
                param.Add("@Token", dbType: DbType.String, value: token, direction: ParameterDirection.Input);

                var refreshToken = baseService.GetSingle<RefreshToken>("sp_UserToken_GetByToken", param);

                // return null if token is no longer active
                if (!refreshToken.IsActive) return null;

                // replace old refresh token with a new one and save
                var newRefreshToken = GenerateRefreshToken(user.Id, ipAddress);

                //refreshToken.Revoked = DateTime.UtcNow;
                //refreshToken.RevokedByIp = ipAddress;
                refreshToken.ReplacedByToken = newRefreshToken.Token;

                // generate new jwt
                var jwtToken = GenerateJwtToken(user);
                refreshToken.Token = jwtToken;

                param = new DynamicParameters();
                param.Add("@Id", dbType: DbType.Int64, value: refreshToken.Id, direction: ParameterDirection.Input);
                param.Add("@UserId", dbType: DbType.Int64, value: refreshToken.UserId, direction: ParameterDirection.Input);
                param.Add("@Token", dbType: DbType.String, value: refreshToken.Token, direction: ParameterDirection.Input);
                param.Add("@Expires", dbType: DbType.DateTime, value: refreshToken.Expires, direction: ParameterDirection.Input);
                param.Add("@Created", dbType: DbType.DateTime, value: refreshToken.Created, direction: ParameterDirection.Input);
                param.Add("@CreatedByIp", dbType: DbType.String, value: refreshToken.CreatedByIp, direction: ParameterDirection.Input);
                param.Add("@Revoked", dbType: DbType.DateTime, value: refreshToken.Revoked, direction: ParameterDirection.Input);
                param.Add("@RevokedByIp", dbType: DbType.String, value: refreshToken.RevokedByIp, direction: ParameterDirection.Input);
                param.Add("@ReplacedByToken", dbType: DbType.String, value: refreshToken.ReplacedByToken, direction: ParameterDirection.Input);
                baseService.Update("sp_UserToken_InsertOrUpdate", param);

                return new AuthenticateResponse(ActionResultData.Success, user, jwtToken, refreshToken.Token, new List<FunctionModel>());
            }
            catch (Exception ex)
            {
                return new AuthenticateResponse() { ActionResult = ActionResultData.Failed, ErrorData = new ErrorDataModel(ex)  };
            }

        }
        public bool RevokeToken(string token, string ipAddress)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@Token", dbType: DbType.String, value: token, direction: ParameterDirection.Input);

                var user = baseService.GetSingle<UserModel>("sp_User_GetByToken", param);

                // return false if no user found with token
                if (user == null) return false;

                param = new DynamicParameters();
                param.Add("@Token", dbType: DbType.String, value: token, direction: ParameterDirection.Input);

                var refreshToken = baseService.GetSingle<RefreshToken>("sp_UserToken_GetByToken", param);

                // return false if token is not active
                if (!refreshToken.IsActive) return false;

                // revoke token and save
                refreshToken.Revoked = DateTime.UtcNow;
                refreshToken.RevokedByIp = ipAddress;

                param = new DynamicParameters();
                param.Add("@Id", dbType: DbType.Int64, value: refreshToken.Id, direction: ParameterDirection.Input);
                param.Add("@UserId", dbType: DbType.Int64, value: refreshToken.UserId, direction: ParameterDirection.Input);
                param.Add("@Token", dbType: DbType.String, value: refreshToken.Token, direction: ParameterDirection.Input);
                param.Add("@Expires", dbType: DbType.DateTime, value: refreshToken.Expires, direction: ParameterDirection.Input);
                param.Add("@Created", dbType: DbType.DateTime, value: refreshToken.Created, direction: ParameterDirection.Input);
                param.Add("@CreatedByIp", dbType: DbType.String, value: refreshToken.CreatedByIp, direction: ParameterDirection.Input);
                param.Add("@Revoked", dbType: DbType.DateTime, value: refreshToken.Revoked, direction: ParameterDirection.Input);
                param.Add("@RevokedByIp", dbType: DbType.String, value: refreshToken.RevokedByIp, direction: ParameterDirection.Input);
                param.Add("@ReplacedByToken", dbType: DbType.String, value: refreshToken.ReplacedByToken, direction: ParameterDirection.Input);
                baseService.Update("sp_UserToken_InsertOrUpdate", param);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public ResponseData GetAllByTaxcode(long id, string taxcode, string search)
        {
            ResponseData response = new ResponseData();
            try
            {
                var param = new DynamicParameters();
                param.Add("@Id", dbType: DbType.Int64, value: id, direction: ParameterDirection.Input);
                param.Add("@Taxcode", dbType: DbType.String, value: taxcode, direction: ParameterDirection.Input);
                param.Add("@Search", dbType: DbType.String, value: search, direction: ParameterDirection.Input);
                response.ActionResult = ActionResultData.Success;
                response.ActionData = baseService.GetList<Object>("sp_User_GetList_ByTaxcode", param: param);
            }
            catch (Exception ex)
            {
                response.ActionResult = ActionResultData.Failed;
                response.ErrorData = new ErrorDataModel(ex) ;
            }
            return response;
        }
        public ResponseData GetById(long id)
        {
            ResponseData response = new ResponseData();
            try
            {
                response.ActionResult = ActionResultData.Success;
                response.ActionData = baseService.GetById<UserModel>("sp_User_GetById", id);
            }
            catch (Exception ex)
            {
                response.ActionResult = ActionResultData.Failed;
                response.ErrorData = new ErrorDataModel(ex) ;
            }
            return response;
        }
        public ResponseData GetByToken(String token)
        {
            ResponseData response = new ResponseData();
            try
            {
                var param = new DynamicParameters();
                param.Add("@Token", dbType: DbType.String, value: token, direction: ParameterDirection.Input);
                response.ActionResult = ActionResultData.Success;
                response.ActionData = baseService.GetSingle<UserModel>("sp_User_GetByToken", param);
            }
            catch (Exception ex)
            {
                response.ActionResult = ActionResultData.Failed;
                response.ErrorData = new ErrorDataModel(ex) ;
            }
            return response;
        }
        public ResponseData GetActive(long id, string token)
        {
            ResponseData response = new ResponseData();
            try
            {
                var param = new DynamicParameters();
                param.Add("@Id", dbType: DbType.Int64, value: id, direction: ParameterDirection.Input);
                param.Add("@Token", dbType: DbType.String, value: token, direction: ParameterDirection.Input);
                response.ActionResult = ActionResultData.Success;
                response.ActionData = baseService.GetSingle<UserModel>("sp_User_GetActive", param);
            }
            catch (Exception ex)
            {
                response.ActionResult = ActionResultData.Failed;
                response.ErrorData = new ErrorDataModel(ex) ;
            }
            return response;
        }
        public ResponseData Create(UserModel model, long userId)
        {
            ResponseData response = new ResponseData();
            try
            {
                var param = new DynamicParameters();
                string hash = StringUtils.CreateHash();
                string pass = StringUtils.Encrypt(model.Password, hash);
                param.Add("@Id", dbType: DbType.Int64, value: model.Id, direction: ParameterDirection.Input);
                param.Add("@FirstName", dbType: DbType.String, value: model.FirstName, direction: ParameterDirection.Input);
                param.Add("@LastName", dbType: DbType.String, value: model.LastName, direction: ParameterDirection.Input);
                param.Add("@UserName", dbType: DbType.String, value: model.Username, direction: ParameterDirection.Input);
                param.Add("@Password", dbType: DbType.String, value: pass, direction: ParameterDirection.Input);
                param.Add("@Hash", dbType: DbType.String, value: hash, direction: ParameterDirection.Input);
                param.Add("@UserId", dbType: DbType.Int64, value: userId, direction: ParameterDirection.Input);
                param.Add("@CompanyId", dbType: DbType.Int32, value: model.CompanyId, direction: ParameterDirection.Input);
                baseService.Update("sp_User_Create", param);

                response.ActionResult = ActionResultData.Success;
            }
            catch (Exception ex)
            {
                response.ActionResult = ActionResultData.Failed;
                response.ErrorData = new ErrorDataModel(ex) ;
            }

            return response;
        }
        public ResponseData Lock(long id, long userId)
        {
            ResponseData response = new ResponseData();
            try
            {
                var param = new DynamicParameters();
                param.Add("@Id", dbType: DbType.Int64, value: id, direction: ParameterDirection.Input);
                param.Add("@UserId", dbType: DbType.Int64, value: userId, direction: ParameterDirection.Input);

                baseService.Update("sp_User_Lock", param);
                response.ActionResult = ActionResultData.Success;
            }
            catch (Exception ex)
            {
                response.ActionResult = ActionResultData.Failed;
                response.ErrorData = new ErrorDataModel(ex) ;
            }
            return response;
        }
        public ResponseData UnLock(long id, long userId)
        {
            ResponseData response = new ResponseData();
            try
            {
                var param = new DynamicParameters();
                param.Add("@Id", dbType: DbType.Int64, value: id, direction: ParameterDirection.Input);
                param.Add("@UserId", dbType: DbType.Int64, value: userId, direction: ParameterDirection.Input);

                baseService.Update("sp_User_UnLock", param);
                response.ActionResult = ActionResultData.Success;
            }
            catch (Exception ex)
            {
                response.ActionResult = ActionResultData.Failed;
                response.ErrorData = new ErrorDataModel(ex) ;
            }
            return response;
        }
        public ResponseData Delete(long id, long userId)
        {
            ResponseData response = new ResponseData();
            try
            {
                baseService.Delete("sp_User_Delete", id, userId);
                response.ActionResult = ActionResultData.Success;
            }
            catch (Exception ex)
            {
                response.ActionResult = ActionResultData.Failed;
                response.ErrorData = new ErrorDataModel(ex) ;
            }
            return response;
        }
        private string GenerateJwtToken(UserModel user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(15),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        private RefreshToken GenerateRefreshToken(long userId, string ipAddress)
        {
            using (var rngCryptoServiceProvider = new RNGCryptoServiceProvider())
            {
                var randomBytes = new byte[64];
                rngCryptoServiceProvider.GetBytes(randomBytes);
                return new RefreshToken
                {
                    UserId = userId,
                    Token = Convert.ToBase64String(randomBytes),
                    Expires = DateTime.UtcNow.AddDays(7),
                    Created = DateTime.UtcNow,
                    CreatedByIp = ipAddress
                };
            }
        }
        private ResponseData CreateLoginLog(string userName, string sourceIP, string result)
        {
            ResponseData response = new ResponseData();
            try
            {
                var param = new DynamicParameters();
                param.Add("@UserName", dbType: DbType.String, value: userName, direction: ParameterDirection.Input);
                param.Add("@SourceIP", dbType: DbType.String, value: sourceIP, direction: ParameterDirection.Input);
                param.Add("@LoginResult", dbType: DbType.String, value: result, direction: ParameterDirection.Input);

                baseService.Update("sp_User_Log", param);
                response.ActionResult = ActionResultData.Success;
            }
            catch (Exception ex)
            {
                response.ActionResult = ActionResultData.Failed;
                response.ErrorData = new ErrorDataModel(ex) ;
            }
            return response;

        }
        public ResponseData Reset(UserInputModel model, long userId)
        {
            ResponseData response = new ResponseData();
            try
            {
                var param = new DynamicParameters();
                string hash = StringUtils.CreateHash();
                string pass = StringUtils.Encrypt(model.Password, hash);
                param.Add("@Id", dbType: DbType.Int64, value: model.Id, direction: ParameterDirection.Input);
                param.Add("@Password", dbType: DbType.String, value: pass, direction: ParameterDirection.Input);
                param.Add("@Hash", dbType: DbType.String, value: hash, direction: ParameterDirection.Input);
                param.Add("@UserId", dbType: DbType.Int64, value: userId, direction: ParameterDirection.Input);

                baseService.Update("sp_User_ResetPassword", param);
                response.ActionResult = ActionResultData.Success;
            }
            catch (Exception ex)
            {
                response.ActionResult = ActionResultData.Failed;
                response.ErrorData = new ErrorDataModel(ex) ;
            }
            return response;
        }
        public ResponseData ChangePassword(UserChangePasswordModel model, long userId)
        {
            ResponseData response = new ResponseData();
            try
            {
                var user = baseService.GetById<UserModel>("sp_User_GetById", userId);

                // return null if user not found
                if (user == null)
                {
                    response.ActionResult = ActionResultData.Failed;
                    response.ErrorData = new ErrorDataModel() { ErrorCode = "ERROR_CPW_0001", ErrorMessage = "Sai tên người dùng hoặc mật khẩu" };
                    return response;
                }

                if (model.NewPassword != model.RetypeNewPassword)
                {
                    response.ActionResult = ActionResultData.Failed;
                    response.ErrorData = new ErrorDataModel() { ErrorCode = "ERROR_CPW_0002", ErrorMessage = "Mật khẩu mới và nhập lại mật khẩu mới chưa khớp" };
                    return response;
                }

                if (user.Password == StringUtils.Encrypt(model.CurrentPassword, user.Hash))
                {
                    var param = new DynamicParameters();
                    string hash = StringUtils.CreateHash();
                    string pass = StringUtils.Encrypt(model.NewPassword, hash);
                    param.Add("@Id", dbType: DbType.Int64, value: userId, direction: ParameterDirection.Input);
                    param.Add("@Password", dbType: DbType.String, value: pass, direction: ParameterDirection.Input);
                    param.Add("@Hash", dbType: DbType.String, value: hash, direction: ParameterDirection.Input);
                    param.Add("@UserId", dbType: DbType.Int64, value: userId, direction: ParameterDirection.Input);

                    baseService.Update("sp_User_ResetPassword", param);
                    response.ActionResult = ActionResultData.Success;

                    return response;
                }
                else
                {
                    response.ActionResult = ActionResultData.Failed;
                    response.ErrorData = new ErrorDataModel() { ErrorCode = "ERROR_CPW_0001", ErrorMessage = "Sai tên người dùng hoặc mật khẩu" };
                    return response;
                }
            }
            catch (Exception ex)
            {
                response.ActionResult = ActionResultData.Failed;
                response.ErrorData = new ErrorDataModel(ex) ;
                return response;
            }
        }
        public Boolean IsFunctionRight(String DoNotCheckPermission, String functionCode, long userId)
        {
            return true;

            if (DoNotCheckPermission != null && DoNotCheckPermission.Equals("DO_NOT_CHECK")) return true;

            try
            {
                var param = new DynamicParameters();
                param.Add("@FunctionCode", dbType: DbType.String, value: functionCode, direction: ParameterDirection.Input);
                param.Add("@UserId", dbType: DbType.Int64, value: userId, direction: ParameterDirection.Input);
                FunctionModel function = baseService.GetSingle<FunctionModel>("sp_Function_GetFunctionByUser", param);

                return (function == null ? false : function.Accessibly);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
