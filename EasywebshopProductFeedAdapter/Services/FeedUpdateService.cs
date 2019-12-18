using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasywebshopProductFeedAdapter.Services
{
    public class FeedUpdateService
    {
        private readonly ProductListService _productListService;
        private readonly FeedFormatterService _feedAdapterService;
        private readonly FeedWriterService _feedWriterService;


        public FeedUpdateService(ProductListService productListService, FeedFormatterService feedAdapterService, FeedWriterService feedWriterService)
        {
            _productListService = productListService;
            _feedAdapterService = feedAdapterService;
            _feedWriterService = feedWriterService;
        }

        public async Task UpdateFeedAsync()
        {
            var feed = await _productListService.GetProductListAsync();
            feed = _feedAdapterService.Rss20FormatFeed(feed);
            _feedWriterService.Write(feed);
        }
    }
}
