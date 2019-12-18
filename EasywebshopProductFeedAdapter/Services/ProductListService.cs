using EasywebshopProductFeedAdapter.Domain.Agents;
using EasywebshopProductFeedAdapter.Domain.Feeds;
using EasywebshopProductFeedAdapter.Net.Http;
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
        private readonly EasyWebshopFeedDeserializerService _deserializerService;

        public ProductListService(HttpClientInstanceController httpClientInstanceController, EasyWebshopFeedDeserializerService deserializerService, IAgent agent)
        {
            _httpClientController = httpClientInstanceController;
            _deserializerService = deserializerService;
            this._agent = agent;
        }

        public async Task<Feed> GetProductListAsync()
        {
            var response = await _httpClientController.Post(_agent);
            string responseContent = await response.Content.ReadAsStringAsync();
            return _deserializerService.Deserialize(responseContent);
        }
    }
}
