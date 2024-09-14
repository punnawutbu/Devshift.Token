using System.Threading.Tasks;
using Dapper;
using Devshift.Dapper;
using Devshift.Dapper.Models;

namespace Devshift.Token.Repositories
{
    public class ApplicationRepository : BaseRepository, IApplicationRepository
    {
        public ApplicationRepository(string connectionString, bool sslMode, Certificate certs) : base(connectionString, sslMode, certs) { }

        public string CheckApiKey(string apiKey, string systemName, string owner)
        {
            using (var sqlConnection = OpenDbConnection())
            {
                var sql = @"
                    select key
                    from api_key
                    where key = @ApiKey and
                    system_name = @SystemName and
                    owner = @Owner and
                    is_active = true
                ";

                return sqlConnection.QueryFirstOrDefault<string>(sql,
                new
                {
                    ApiKey = apiKey,
                    SystemName = systemName,
                    Owner = owner
                });
            }
        }

        public async Task<string> CheckBlacklistToken(string token, string system)
        {
            using (var sqlConnection = OpenDbConnection())
            {
                var sql = @"
                    select token
                    from blacklist_token
                    where token = @Token and
                    system_name = @SystemName
                ";

                return await sqlConnection.QueryFirstOrDefaultAsync<string>(sql, new { Token = token, SystemName = system });
            }
        }
    }
}