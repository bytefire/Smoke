using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Smoke.Domain
{
    [ServiceContract]
    public interface ILiveScoresService
    {
        [OperationContract]
        FootballScore GetLatestFootballScore(string home, string away);
    }
}
