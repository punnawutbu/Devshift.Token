using System.Collections.Generic;
using System.Threading.Tasks;
using Devshift.Token.Models;

namespace Devshift.Token.Repositories
{
    public interface IElkRepository
    {
        Task<IReadOnlyCollection<ElkProductRes>> CheckBlacklistTokenV2(string token, string system);
    }
}