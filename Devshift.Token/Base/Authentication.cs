using System.Threading.Tasks;
using Devshift.Token.Facades.Base;
using Devshift.Token.Models;

namespace Devshift.Token.Base
{
    public class Authentication : IAuthentication
    {
        protected IAuthorizeFacade _facade;

        public bool CheckApiKey(string apiKey)
        {
            return _facade.CheckAuthorizeApiKey(apiKey);
        }

        public bool CheckApiKey(string apiKey, string systemName, string owner)
        {
            return _facade.CheckAuthorizeApiKey(apiKey, systemName, owner);
        }

        public async Task<UserProfile> CheckUserToken(string token)
        {
            return await _facade.CheckUserToken(token);
        }

        public async Task<VerifyToken> CheckUserToken(string token, string apiKey, string systemName, string owner)
        {
            return await _facade.CheckUserToken(token, apiKey, systemName, owner);
        }

        public async Task<string> CheckUserTokenV2(string token)
        {
            return await _facade.CheckUserTokenV2(token);
        }
    }
}