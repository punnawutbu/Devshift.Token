using System.Threading.Tasks;
using Devshift.Token.Models;

namespace Devshift.Token.Facades.Base
{
    public interface IAuthorizeFacade
    {
        bool CheckAuthorizeApiKey(string apiKey);
        bool CheckAuthorizeApiKey(string apiKey, string systemName, string owner);
        Task<UserProfile> CheckUserToken(string token);
        Task<VerifyToken> CheckUserToken(string token, string apiKey, string systemName, string owner);
        JwtToken CheckJwt(string token);
        Task<string> CheckUserTokenV2(string token);
    }
}