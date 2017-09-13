using DAL.Context;
using Microsoft.Extensions.DependencyInjection;

namespace DAL.ServiceCollectionExtension
{
    public static class ServiceCollectionExtension
    {
        public static void AddEntityFramework(this IServiceCollection services)
        {
            services.AddDbContext<StoreAggregatorContext>();
        }
    }
}