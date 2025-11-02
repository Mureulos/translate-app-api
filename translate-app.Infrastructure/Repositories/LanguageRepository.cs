using Microsoft.EntityFrameworkCore;
using translate_app.Domain.Entities;
using translate_app.Domain.Repositories;
using translate_app.Infrastructure.Data;

namespace translate_app.Infrastructure.Repositories
{
    public class LanguageRepository : ILanguageRepository
    {
        private readonly AppDbContext _context;
        public LanguageRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Language>> GetAllLanguages(CancellationToken cancellationToken)
        {
            return await _context.Set<Language>().ToListAsync();
        }

        public async Task<Language> GetLanguageById(int idLanguage, CancellationToken cancellationToken)
        {
            return await _context.Set<Language>().FirstOrDefaultAsync(lang => lang.Id == idLanguage, cancellationToken);
        }
    }
}
