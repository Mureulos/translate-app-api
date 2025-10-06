using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using translate_app.Domain.Repositories;
using translate_app.Infrastructure.Repositories;

namespace translate_app.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<ITranslationRepository, TranslationRepository>();

            return services;
        }
    }
}
