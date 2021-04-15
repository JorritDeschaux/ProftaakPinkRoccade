using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PinkRoccade.BS.Data;

namespace Pinkroccade.BS
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterDataServices(
            this IServiceCollection services, IConfiguration configuration)
        {

            services.Add(new ServiceDescriptor(typeof(LoginContext), new LoginContext(configuration.GetConnectionString("DefaultConnection"))));

            return services;
        }
    }
}
