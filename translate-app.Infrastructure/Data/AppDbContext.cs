using Microsoft.EntityFrameworkCore;
using translate_app.Domain.Entities;

namespace translate_app.Infrastructure.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options): DbContext(options) 
    {
        public DbSet<Translation> Translation { get; set; } = null!;
        public DbSet<Language> Languages { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DependencyInjection).Assembly);
        }
    }
}
