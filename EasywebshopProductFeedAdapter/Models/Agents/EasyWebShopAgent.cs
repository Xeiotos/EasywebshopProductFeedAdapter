using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasywebshopProductFeedAdapter.Domain.Authorization;
using EasywebshopProductFeedAdapter.Extensions;

namespace EasywebshopProductFeedAdapter.Domain.Agents
{
    public class EasyWebShopAgent : IAgent
    {
        private string _username;
        private string _password;
        public string Token => (_username + ":" + _password).ToBase64();
        public AuthorizationType AuthorizationType { get; }
        public string ConnectionString { get; set; }


        public EasyWebShopAgent()
        {

        }
    }
}
