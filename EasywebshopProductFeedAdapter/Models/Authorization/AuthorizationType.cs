using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasywebshopProductFeedAdapter.Models.Authorization
{
    public sealed class AuthorizationType
    {
        public static readonly AuthorizationType None = new AuthorizationType("None");
        public static readonly AuthorizationType Basic = new AuthorizationType("Basic");
        public static readonly AuthorizationType Bearer = new AuthorizationType("Bearer");

        private AuthorizationType(string value)
        {
            Value = value;
        }

        public string Value { get; private set; }
    }
}
