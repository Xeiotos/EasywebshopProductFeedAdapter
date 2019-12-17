using EasywebshopProductFeedAdapter.Domain.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasywebshopProductFeedAdapter.Domain.Agents
{
    public interface IAgent
    {
        string Token { get; }
        AuthorizationType AuthorizationType { get; }
        string ConnectionString { get; set; }
    }
}
