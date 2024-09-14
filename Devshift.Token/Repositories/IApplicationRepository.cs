using System.Threading.Tasks;

namespace Devshift.Token.Repositories
{
    public interface IApplicationRepository
    {
        Task<string> CheckBlacklistToken(string token, string system);
        string CheckApiKey(string apiKey, string systemName, string owner);
    }
}