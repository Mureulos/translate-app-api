﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using translate_app.Domain.Repositories;
using translate_app.Domain.Services;
using translate_app.Infrastructure.Data;
using translate_app.Infrastructure.Repositories;
using translate_app.Infrastructure.Services;

namespace translate_app.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(
                    configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly("translate-app.Infrastructure")
                )
            );

            services.AddTransient<ILanguageRepository, LanguageRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<ITranslationRepository, TranslationRepository>();
            services.AddTransient<ITranslationRecordRepository, TranslationRecordRepository>();

            services.Configure<TranslationCacheOptions>(
                configuration.GetSection(TranslationCacheOptions.SectionName));
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration["Redis:ConnectionString"]
                    ?? throw new InvalidOperationException("Redis:ConnectionString must be configured.");
            });
            services.AddSingleton<ITranslationCache, RedisTranslationCache>();

            return services;
        }
    }
}
