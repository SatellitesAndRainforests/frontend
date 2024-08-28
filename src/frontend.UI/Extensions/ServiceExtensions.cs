using frontend.Application.Interfaces;
using frontend.Application.Mappings;
using frontend.Application.Services;
using frontend.Infrastructure.ApiClients;

namespace frontend.UI.Extensions
{
    public static class ServiceExtensions
    {

        public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddAutoMapper(typeof(MappingProfile));

            services.AddTransient<ITestService, TestService>();
            services.AddHttpClient<ITestClient, TestClient>( x => x.BaseAddress = new Uri("https://localhost:7141/"));

            return services;
        }

    }
}
