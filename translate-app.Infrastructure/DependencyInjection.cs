using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using translate_app.Domain.Repositories;
using translate_app.Infrastructure.Data;
using translate_app.Infrastructure.Repositories;

namespace translate_app.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly("translate-app.Infrastructure")
                )
            );

            services.AddTransient<ITranslationRepository, TranslationRepository>();
            services.AddTransient<ILanguageRepository, LanguageRepository>();
            services.AddTransient<IUserRepository, UserRepository>();

            return services;
        }
    }
}
