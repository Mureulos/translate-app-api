using Microsoft.EntityFrameworkCore;

namespace translate_app.Infrastructure.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options): DbContext(options) 
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
            => modelBuilder.ApplyConfigurationsFromAssembly(typeof(DependencyInjection).Assembly);
    }

}
