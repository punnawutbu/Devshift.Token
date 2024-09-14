using Devshift.Token.Base;
using Devshift.Token.Facades;
using Devshift.Token.Models;

namespace Devshift.Token
{
    public class AppAuthentication : Authentication
    {
        public AppAuthentication(AuthenticationConfig config)
        {
            _facade = new AppAuthorizeFacade(config);
        }
    }
}