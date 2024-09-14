using System.Threading.Tasks;
using Devshift.Token.Models;

namespace Devshift.Token.Facades.Base
{
    public interface IDataFacade
    {
        Task<bool> CheckBlacklistToken(string token, string system);
        Task<bool> CheckBlacklistTokenV2(string token, string system);
        string Base64UrlEncode(byte[] input);
        Task<UserProfile> GetUserProfile(JwtToken jwt);
        string CheckApiKey(string apiKey, string systemName, string owner);
    }
}