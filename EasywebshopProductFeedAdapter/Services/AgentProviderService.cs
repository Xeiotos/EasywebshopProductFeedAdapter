using EasywebshopProductFeedAdapter.Domain.Agents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasywebshopProductFeedAdapter.Services
{
    public class AgentProviderService
    {
        static readonly IAgent _agent = new EasyWebShopAgent();

        public IAgent Agent => _agent;
    }
}
