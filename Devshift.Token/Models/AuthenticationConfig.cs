
namespace Devshift.Token.Models
{
    public class AuthenticationConfig
    {
        public int LiftTime { get; set; }
        public string SecertToken { get; set; }
        public string Aud { get; set; }
        public string ApplicationConnectionString { get; set; }
        public bool ApplicationSslMode { get; set; } = false;
        public Certificate ApplicationCertificate { get; set; }
        public ElasticClient ElasticsearchClient { get; set; }
    }

    public class GmarkAuthenticationConfig : AuthenticationConfig
    {
        public string ConnectionString { get; set; }
        public bool SslMode { get; set; } = false;
        public Certificate Certificate { get; set; }
    }

    public class N1AuthenticationConfig : AuthenticationConfig
    {
        public string ConnectionString { get; set; }
        public bool SslMode { get; set; } = false;
        public Certificate Certificate { get; set; }
    }
    public class ConnectionDbConfig
    {
        public string ConnectionString { get; set; }
        public bool SslMode { get; set; } = false;
        public Certificate Certificate { get; set; }
    }
}