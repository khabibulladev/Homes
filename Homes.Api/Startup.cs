using Homes.Api.Brokers.Storages;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Homes.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration) =>
            Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var apiInfo = new OpenApiInfo
            {
                Title = "Homes.Api",
                Version = "v1"
            };
            services.AddControllers();
            services.AddDbContext<StorageBroker>();
            services.AddTransient<IStorageBroker, StorageBroker>();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc(
                    name: "v1",
                    info: apiInfo);
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment environment)
        {
            if (environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();

                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint(
                        url: "/swagger/v1/swagger.json",
                        name: "Homes.Api v1");
                });
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
                endpoints.MapControllers());
        }
    }
}
