using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AdventureWorks.Web.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using AzureStorage;
using AzureSearch;
using Microsoft.Azure.KeyVault;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace AdventureWorks.Web
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
            services.Configure<CookiePolicyOptions>(options =>
                {
                    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                    options.CheckConsentNeeded = context => true;
                    options.MinimumSameSitePolicy = SameSiteMode.None;
                });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            var connection = this.Configuration.GetConnectionString("AdventureWorks2017Context");
            services.AddDbContext<AdventureWorks2017Context>(options => options.UseSqlServer(connection));

            var azureName = Configuration.GetValue<string>("AzureStorage:Name");
            var azureKey = Configuration.GetValue<string>("AzureStorage:Key");
            services.AddScoped<IAzureService>(s => new AzureService(azureName, azureKey));

            var azureSearchName = Configuration.GetValue<string>("AzureSearch:Name");
            var azureSearchKey = Configuration.GetValue<string>("AzureSearch:Key");
            services.AddScoped<IAzureSearchService>(s => new AzureSearchService(azureSearchName, azureSearchKey));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
