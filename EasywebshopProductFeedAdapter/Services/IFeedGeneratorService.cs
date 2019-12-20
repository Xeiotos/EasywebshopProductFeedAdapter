using EasywebshopProductFeedAdapter.Domain.Feeds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasywebshopProductFeedAdapter.Services
{
    public interface IFeedGeneratorService
    {
        public Task<Feed> GetProductFeedAsync();
    }
}
