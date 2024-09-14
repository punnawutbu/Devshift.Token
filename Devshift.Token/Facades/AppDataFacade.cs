using System.Threading.Tasks;
using Devshift.Dapper.Models;
using Devshift.Token.Facades.Base;
using Devshift.Token.Models;
using Devshift.Token.Repositories;
using Nest;

namespace Devshift.Token.Facades
{
    public class AppDataFacade : DataFacade
    {
        public AppDataFacade(string applicationConnectionString, bool sslMode, Certificate certificate, ElasticClient _client)
        {
            _applicationRepository = new ApplicationRepository(applicationConnectionString, sslMode, certificate);
            _elkRepository = new ElkRepository(_client);
        }

        public AppDataFacade(ElasticClient elasticClient, IElkRepository elkRepository)
        {
            _elkRepository = elkRepository;
        }

        public override Task<UserProfile> GetUserProfile(JwtToken jwt)
        {
            throw new System.NotImplementedException();
        }
    }
}