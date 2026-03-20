using Microsoft.EntityFrameworkCore;
using translate_app.Domain.Entities;
using translate_app.Domain.Repositories;
using translate_app.Infrastructure.Data;

namespace translate_app.Infrastructure.Repositories;

public class TranslationRepository : ITranslationRepository
{
    private readonly AppDbContext _context;

    public TranslationRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Translation> SaveTranslation(
        Translation translation,
        CancellationToken cancellationToken = default)
    {
        await _context.Translation.AddAsync(translation, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return translation;
    }
    
    public async Task<IEnumerable<Translation>> GetSavedTranslations(int userId, CancellationToken cancellationToken = default)
    {
        return await _context.Translation
            .AsNoTracking()
            .Where(t => t.UserId == userId)
            .ToListAsync(cancellationToken);
    }
}