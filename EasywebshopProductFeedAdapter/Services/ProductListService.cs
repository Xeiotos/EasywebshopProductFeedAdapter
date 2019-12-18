using EasywebshopProductFeedAdapter.Domain.Agents;
using EasywebshopProductFeedAdapter.Domain.Feeds;
using EasywebshopProductFeedAdapter.Net.Http;
using EasywebshopProductFeedAdapter.Util.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasywebshopProductFeedAdapter.Services
{
    public class ProductListService
    {

        private readonly IAgent _agent;
        private readonly HttpClientInstanceController _httpClientController;

        public ProductListService(HttpClientInstanceController httpClientInstanceController, IAgent agent)
        {
            _httpClientController = httpClientInstanceController;
            this._agent = agent;
        }

        public async Task<Feed> GetProductListAsync()
        {
            var feedSerializer = new EasyWebshopFeedDeserializer();
            var response = await _httpClientController.Post(_agent);
            string responseContent = await response.Content.ReadAsStringAsync();
            return feedSerializer.Deserialize(responseContent);
        }
    }
}
