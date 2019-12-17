using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasywebshopProductFeedAdapter.Services
{
    public class FeedUpdateService
    {
        private readonly ProductListService _productListService;
        private readonly FeedAdapterService _feedAdapterService;
        private readonly FeedWriterService _feedWriterService;


        public FeedUpdateService(ProductListService productListService, FeedAdapterService feedAdapterService, FeedWriterService feedWriterService)
        {
            _productListService = productListService;
            _feedAdapterService = feedAdapterService;
            _feedWriterService = feedWriterService;
        }

        public async Task UpdateFeedAsync()
        {
            var feed = await _productListService.GetProductListAsync();
            feed = _feedAdapterService.AdaptEasyWebshopToGoogleMerchant(feed);
            _feedWriterService.Write(feed);
        }
    }
}
