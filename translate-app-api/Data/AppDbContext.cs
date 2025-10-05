using Microsoft.EntityFrameworkCore;

namespace translate_app_api.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options): DbContext(options) 
    {


        protected override void OnModelCreating(ModelBuilder modelBuilder)
            => modelBuilder.ApplyConfigurationsFromAssembly(typeof(Program).Assembly);
    }

}
