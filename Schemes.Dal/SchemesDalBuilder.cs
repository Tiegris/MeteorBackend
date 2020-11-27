using Microsoft.Extensions.DependencyInjection;

namespace Schemes.Dal
{
    public static class SchemesDalBuilder
    {
        public static void AddSchemesDal(this IServiceCollection services) {
            services.AddTransient<ISchemesRepository, SchemesRepository>();
        }
    }
}
