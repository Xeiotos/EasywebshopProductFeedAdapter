using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasywebshopProductFeedAdapter.Models.Authorization;
using EasywebshopProductFeedAdapter.Extensions;

namespace EasywebshopProductFeedAdapter.Domain.Agents
{
    public class EasyWebShopAgent : IAgent
    {
        private string _username;
        private string _password;
        public string Token => (_username + ":" + _password).ToBase64();
        public AuthorizationType AuthorizationType { get; private set; }
        public string ConnectionString { get; private set; }


        public EasyWebShopAgent(string username, string password, AuthorizationType authorizationType, string connectionString)
        {
            _username = username;
            _password = password;
            AuthorizationType = authorizationType;
            ConnectionString = connectionString;
        }
    }
}
