using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasywebshopProductFeedAdapter.Domain.Agents;
using EasywebshopProductFeedAdapter.Extensions;
using EasywebshopProductFeedAdapter.Models.Authorization;
using EasywebshopProductFeedAdapter.Net.Http;
using EasywebshopProductFeedAdapter.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace EasywebshopProductFeedAdapter
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<HttpClientInstanceController>();
            services.AddSingleton<IConfiguration>(Configuration);
            services.AddSingleton<IFeedWriterService, FeedWriterService>();

            services.AddFeedGenerator(options =>
            {
                options.Agent = new EasyWebShopAgent(
                    Configuration.GetValue<string>("Username"), 
                    Configuration.GetValue<string>("Password"), 
                    AuthorizationType.Basic, 
                    Configuration.GetValue<string>("Easywebshop:API_URL")
                );
            });

            services.AddSingleton<EasyWebshopFeedDeserializerService>();
            services.AddSingleton<IHostedService, FeedUpdateService>();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseStaticFiles();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
