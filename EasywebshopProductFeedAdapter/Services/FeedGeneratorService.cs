using EasywebshopProductFeedAdapter.Domain.Agents;
using EasywebshopProductFeedAdapter.Domain.Feeds;
using EasywebshopProductFeedAdapter.Exceptions;
using EasywebshopProductFeedAdapter.Net.Http;
using EasywebshopProductFeedAdapter.Services.Options;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasywebshopProductFeedAdapter.Services
{
    public class FeedGeneratorService : IFeedGeneratorService
    {

        private readonly IAgent _agent;
        private readonly HttpClientInstanceController _httpClientController;
        private readonly EasyWebshopFeedDeserializerService _deserializerService;

        public FeedGeneratorService(HttpClientInstanceController httpClientInstanceController, EasyWebshopFeedDeserializerService deserializerService, IOptions<FeedGeneratorServiceOptions> options)
        {
            _httpClientController = httpClientInstanceController;
            _deserializerService = deserializerService;
            _agent = options.Value.Agent;
        }

        public async Task<Feed> GetProductFeedAsync()
        {
            var response = await _httpClientController.Get(_agent);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string responseContent = await response.Content.ReadAsStringAsync();
                return _deserializerService.Deserialize(responseContent);
            }
            else
            {
                throw new HttpException(response.StatusCode);
            }
        }
    }
}
