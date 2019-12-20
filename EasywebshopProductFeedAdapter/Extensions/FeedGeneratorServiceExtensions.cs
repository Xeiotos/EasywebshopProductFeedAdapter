using EasywebshopProductFeedAdapter.Services;
using EasywebshopProductFeedAdapter.Services.Options;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasywebshopProductFeedAdapter.Extensions
{
    public static class FeedGeneratorServiceExtensions
    {
        public static IServiceCollection AddFeedGenerator(this IServiceCollection serviceCollection, Action<FeedGeneratorServiceOptions> options)
        {
            serviceCollection.AddSingleton<IFeedGeneratorService, FeedGeneratorService>();
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options),
                    @"You must provide options for FeedGeneratorService.");
            }
            serviceCollection.Configure(options);
            return serviceCollection;
        }
    }
}
