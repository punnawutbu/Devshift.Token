using System.Threading.Tasks;
using Devshift.Token.Models;

namespace Devshift.Token.Base
{
    public interface IAuthentication
    {
        bool CheckApiKey(string apiKey);
        bool CheckApiKey(string apiKey, string systemName, string owner);
        Task<UserProfile> CheckUserToken(string token);
        Task<VerifyToken> CheckUserToken(string token, string apiKey, string systemName, string owner);
        Task<string> CheckUserTokenV2(string token);
    }
}