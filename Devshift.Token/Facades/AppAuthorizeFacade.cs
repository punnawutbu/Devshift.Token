using Devshift.Token.Facades.Base;
using Devshift.Token.Models;

namespace Devshift.Token.Facades
{
    public class AppAuthorizeFacade : AuthorizeFacade
    {
        public AppAuthorizeFacade(AuthenticationConfig config)
        {
            _secertToken = config.SecertToken;
            _aud = config.Aud;
            _liftTime = config.LiftTime;
            _dataFacade = new AppDataFacade(config.ApplicationConnectionString, config.ApplicationSslMode, config.ApplicationCertificate, config.ElasticsearchClient);
        }
    }
}