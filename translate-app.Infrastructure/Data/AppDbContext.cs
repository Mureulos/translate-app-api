using Microsoft.EntityFrameworkCore;
using translate_app.domain.Models;

namespace translate_app.Infrastructure.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options): DbContext(options) 
    {
        public DbSet<TranslationRequest> Translation { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DependencyInjection).Assembly);
        }
    }
}
