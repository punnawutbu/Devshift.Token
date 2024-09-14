using System;
using System.Linq;
using System.Threading.Tasks;
using Devshift.Token.Models;
using Devshift.Token.Repositories;

namespace Devshift.Token.Facades.Base
{
    public abstract class DataFacade : IDataFacade
    {
        protected IApplicationRepository _applicationRepository;
        protected IElkRepository _elkRepository;

        public string Base64UrlEncode(byte[] input)
        {
            var output = Convert.ToBase64String(input);
            output = output.Split('=')[0]; // Remove any trailing '='s
            output = output.Replace('+', '-'); // 62nd char of encoding
            output = output.Replace('/', '_'); // 63rd char of encoding
            return output;
        }

        public string CheckApiKey(string apiKey, string systemName, string owner)
        {
            return _applicationRepository.CheckApiKey(apiKey, systemName, owner);
        }

        public async Task<bool> CheckBlacklistToken(string token, string system)
        {
            var blacklistToken = await _applicationRepository.CheckBlacklistToken(token, system);
            return !string.IsNullOrEmpty(blacklistToken);
        }

        public async Task<bool> CheckBlacklistTokenV2(string token, string system)
        {
            var blacklistToken = await _elkRepository.CheckBlacklistTokenV2(token, system);
            return (blacklistToken.Count > 0);
        }

        public abstract Task<UserProfile> GetUserProfile(JwtToken jwt);
    }
}