using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Schemes.Api
{
    public static class CacheBuilder
    {
        public static void AddRedisCache(this IServiceCollection services, IConfiguration config) {
            var redisUrl = config.GetValue<string>("RedisUrl") ?? "redis:6379";

            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = redisUrl;
                options.InstanceName = "schemes";
            });

            services.AddTransient<SchemeCache>();
        }
    }
}
