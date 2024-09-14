using System.Collections.Generic;
using System.Threading.Tasks;
using Devshift.Token.Models;
using Nest;

namespace Devshift.Token.Repositories
{
    public class ElkRepository : IElkRepository
    {
        private readonly IElasticClient _client;

        public ElkRepository(IElasticClient client)  {
            _client = client;
        }

        public async Task<IReadOnlyCollection<ElkProductRes>> CheckBlacklistTokenV2(string token, string system)
        {
            var resp = await _client.SearchAsync<ElkProductRes>(x => x
                .Size(0)
                .Query(q => q
                    .Bool(b => b
                        .Must(m => m
                            .MatchPhrase(mp => mp
                                .Field("systemname")
                                .Query(system)
                            ) && q
                            .MatchPhrase(mp => mp
                                .Field("token")
                                .Query(token)
                            )
                        )
                    )
                ));

            var documents = resp.Documents;
            return documents;
        }
    }
}