using EasywebshopProductFeedAdapter.Domain.Feeds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasywebshopProductFeedAdapter.Services
{
    public interface IFeedWriterService
    {
        public void Write(Feed feed);
    }
}
