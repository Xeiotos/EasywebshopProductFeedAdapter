using EasywebshopProductFeedAdapter.Domain.Agents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasywebshopProductFeedAdapter.Services.Options
{
    public class FeedGeneratorServiceOptions
    {
        public IAgent Agent { get; set; }
    }
}
