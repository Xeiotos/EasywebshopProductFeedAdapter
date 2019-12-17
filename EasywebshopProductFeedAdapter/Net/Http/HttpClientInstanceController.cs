using EasywebshopProductFeedAdapter.Domain.Agents;
using EasywebshopProductFeedAdapter.Domain.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace EasywebshopProductFeedAdapter.Net.Http
{
    public class HttpClientInstanceController
    {
        static readonly HttpClient httpClient = new HttpClient();

        public Task<HttpResponseMessage> Post(IAgent agent)
        {
            using (var requestMessage = new HttpRequestMessage(HttpMethod.Post, agent.ConnectionString))
            {
                if (agent.AuthorizationType != AuthorizationType.None)
                {
                    requestMessage.Headers.Authorization = 
                        new AuthenticationHeaderValue(agent.AuthorizationType.Value, agent.Token);
                }

                return httpClient.SendAsync(requestMessage); //Return response
            }
        }
    }
}
