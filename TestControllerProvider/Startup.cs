using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Providers;
using TestControllerProvider.Controllers;

namespace TestControllerProvider
{
    public class Startup
    {
        private GenericControllerFeatureProvider GenericControllersProvider =>
            new GenericControllerFeatureBuilder(typeof(BaseCrudController<>))
                .AddController(nameof(WeatherForecast), typeof(WeatherForecast).GetTypeInfo())
                .AddController(nameof(MoneyForecast), typeof(MoneyForecast).GetTypeInfo())
                .Build();

        public void ConfigureServices(IServiceCollection services) =>
            services.AddControllers()
                    .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                    .ConfigureApplicationPartManager(apm =>
                        apm.FeatureProviders.Add(GenericControllersProvider));
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) =>
            app.UseHttpsRedirection()
                .UseRouting()
                .UseAuthorization()
                .UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
}
