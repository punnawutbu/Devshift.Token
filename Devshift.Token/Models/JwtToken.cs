
namespace Devshift.Token.Models
{
    public class JwtToken
    {
        public string Aud { get; set; }
        public string Iss { get; set; }
        public string Nbf { get; set; }
        public string Exp { get; set; }
        public string Mobile { get; set; }
        public string CountryCode { get; set; }
        public string Role { get; set; }
        public string IdNumber { get; set; }
    }
    public class VerifyToken
    {
        public string Role { get; set; }
        public bool Verfiy { get; set; }
    }
}