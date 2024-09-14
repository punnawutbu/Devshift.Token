using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Devshift.Token.Models;
using Devshift.Token.Providers;

namespace Devshift.Token.Facades.Base
{
    public class AuthorizeFacade : IAuthorizeFacade
    {
        protected int _liftTime;
        protected string _secertToken;
        protected string _aud;
        protected IDataFacade _dataFacade;
        protected IDateTimeProvider _dateTimeProvider = new DateTimeProvider();

        public bool CheckAuthorizeApiKey(string apiKey)
        {
            if (string.IsNullOrEmpty(apiKey)) return false;

            var jwt = CheckJwt(apiKey);

            if (jwt == null) return false;
            if (jwt.Aud != _aud) return false;

            return true;
        }

        public bool CheckAuthorizeApiKey(string apiKey, string systemName, string owner)
        {
            if (string.IsNullOrEmpty(apiKey) ||
            string.IsNullOrEmpty(systemName) ||
            string.IsNullOrEmpty(owner)) return false;

            var key = _dataFacade.CheckApiKey(apiKey, systemName, owner);
            if (string.IsNullOrEmpty(key)) return false;

            return true;
        }

        public JwtToken CheckJwt(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var readToken = handler.CanReadToken(token);

            if (!readToken) return null;


            var jwt = handler.ReadJwtToken(token);
            var aud = _GetClaimValue(jwt, "aud");
            var iss = _GetClaimValue(jwt, "iss");
            var mobile = _GetClaimValue(jwt, "mobile");
            var countryCode = _GetClaimValue(jwt, "countryCode");
            var exp = _GetClaimValue(jwt, "exp");
            var nbf = _GetClaimValue(jwt, "nbf");
            var role = _GetClaimValue(jwt, "role");
            var idNumber = _GetClaimValue(jwt, "idNumber");


            return new JwtToken
            {
                Aud = aud,
                Iss = iss,
                Mobile = mobile,
                CountryCode = countryCode,
                Nbf = nbf,
                Exp = exp,
                Role = role,
                IdNumber = idNumber
            };
        }

        public async Task<UserProfile> CheckUserToken(string token)
        {
            var jwt = await _VerifyUserToken(token);

            if (jwt == null) return null;

            var res = await _dataFacade.GetUserProfile(jwt);

            return res;
        }

        public async Task<VerifyToken> CheckUserToken(string token, string apiKey, string systemName, string owner)
        {
            var noneToken = string.IsNullOrEmpty(token);
            var noneApiKey = string.IsNullOrEmpty(apiKey);
            var noneSystemName = string.IsNullOrEmpty(systemName);
            var noneOwner = string.IsNullOrEmpty(owner);

            var resp = new VerifyToken();

            if (noneToken || noneApiKey || noneSystemName || noneOwner) return resp;

            var verifyApiKey = CheckAuthorizeApiKey(apiKey, systemName, owner);

            if (!verifyApiKey) return resp;

            var jwt = await _VerifyUserToken(token);

            if (jwt == null) return resp;

            resp.Role = jwt.Role;
            resp.Verfiy = true;

            return resp;
        }

        public async Task<string> CheckUserTokenV2(string token)
        {

            if (string.IsNullOrEmpty(token)) return "null1";

            var jwt = CheckJwt(token);

            if (jwt == null) return "null2";

            string system = (!string.IsNullOrEmpty(jwt.Iss)) ? jwt.Iss : jwt.Aud;

            var blacklistToken = await _dataFacade.CheckBlacklistToken(token, system);

            if (blacklistToken) return "blacklist";

            DateTime now = _dateTimeProvider.UtcNow();
            DateTime expireDate = new DateTime(1970, 1, 1);

            try
            {
                expireDate = expireDate.AddSeconds(int.Parse(jwt.Exp));
            }
            catch (Exception)
            {
                return "exception";
            }

            if (now > expireDate) return "expire";

            return "success";
        }

        #region private
        private bool VerifySignature(string token)
        {

            string jwt = token;
            string[] parts = jwt.Split(".".ToCharArray());
            var header = parts[0];
            var payload = parts[1];
            var signature = parts[2];//Base64UrlEncoded signature from the token

            byte[] bytesToSign = Encoding.UTF8.GetBytes(string.Join(".", header, payload));

            byte[] secret = Encoding.UTF8.GetBytes(_secertToken);

            var alg = new HMACSHA256(secret);
            var hash = alg.ComputeHash(bytesToSign);

            var computedSignature = _dataFacade.Base64UrlEncode(hash);

            if (signature != computedSignature)
            {
                return false;
            }

            return true;

        }

        private string _GetClaimValue(JwtSecurityToken jwt, string claimType)
        {
            var claim = jwt.Claims.FirstOrDefault(claim => claim.Type == claimType);

            return (claim == null) ? null : claim.Value;
        }
        private async Task<JwtToken> _VerifyUserToken(string token)
        {
            if (string.IsNullOrEmpty(token)) return null;

            var jwt = CheckJwt(token);

            if (jwt == null) return null;

            string system = (!string.IsNullOrEmpty(jwt.Iss)) ? jwt.Iss : jwt.Aud;

            var blacklistToken = await _dataFacade.CheckBlacklistToken(token, system);

            if (blacklistToken) return null;

            DateTime now = _dateTimeProvider.UtcNow();
            DateTime expireDate = new DateTime(1970, 1, 1);

            try
            {
                expireDate = expireDate.AddSeconds(int.Parse(jwt.Exp));
            }
            catch (Exception)
            {
                return null;
            }

            if (now > expireDate) return null;
            if (!VerifySignature(token)) return null;

            return jwt;
        }
        #endregion
    }
}