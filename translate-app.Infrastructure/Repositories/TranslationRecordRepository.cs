using Microsoft.EntityFrameworkCore;
using translate_app.Domain.Entities;
using translate_app.Domain.Repositories;
using translate_app.Domain.Services;
using translate_app.Infrastructure.Data;

namespace translate_app.Infrastructure.Repositories;

public sealed class TranslationRecordRepository(AppDbContext context)
    : ITranslationRecordRepository
{
    public Task<TranslationRecord?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken = default) =>
        context.TranslationRecords
            .AsNoTracking()
            .SingleOrDefaultAsync(record => record.Id == id, cancellationToken);

    public Task<TranslationRecord?> GetByIdentityAsync(
        TranslationMemoryIdentity identity,
        CancellationToken cancellationToken = default) =>
        context.TranslationRecords
            .AsNoTracking()
            .SingleOrDefaultAsync(record =>
                record.NormalizedSource == identity.NormalizedSource &&
                record.TargetLanguage == identity.TargetLanguage &&
                record.ContextTag == identity.ContextTag,
                cancellationToken);

    public async Task<TranslationRecord> CreateHumanVerifiedAsync(
        TranslationMemoryIdentity identity,
        string translatedText,
        CancellationToken cancellationToken = default)
    {
        var record = new TranslationRecord
        {
            NormalizedSource = identity.NormalizedSource,
            TargetLanguage = identity.TargetLanguage,
            ContextTag = identity.ContextTag,
            TranslatedText = translatedText.Trim(),
            IsHumanVerified = true,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        await context.TranslationRecords.AddAsync(record, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return record;
    }

    public async Task<TranslationRecord?> UpdateHumanVerifiedAsync(
        int id,
        TranslationMemoryIdentity identity,
        string translatedText,
        CancellationToken cancellationToken = default)
    {
        var record = await context.TranslationRecords
            .SingleOrDefaultAsync(item => item.Id == id, cancellationToken);

        if (record is null)
            return null;

        record.NormalizedSource = identity.NormalizedSource;
        record.TargetLanguage = identity.TargetLanguage;
        record.ContextTag = identity.ContextTag;
        record.TranslatedText = translatedText.Trim();
        record.IsHumanVerified = true;
        record.UpdatedAt = DateTime.UtcNow;

        await context.SaveChangesAsync(cancellationToken);
        return record;
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        await context.TranslationRecords
            .Where(record => record.Id == id)
            .ExecuteDeleteAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<TranslationRecord>> SearchAsync(
        string? targetLanguage,
        string? contextTag,
        bool? isHumanVerified,
        string? query,
        int skip,
        int take,
        CancellationToken cancellationToken = default)
    {
        var records = context.TranslationRecords.AsNoTracking().AsQueryable();

        if (!string.IsNullOrWhiteSpace(targetLanguage))
            records = records.Where(record =>
                record.TargetLanguage == SmartTranslationRequest.Normalize(targetLanguage));

        if (!string.IsNullOrWhiteSpace(contextTag))
            records = records.Where(record =>
                record.ContextTag == SmartTranslationRequest.NormalizeContext(contextTag));

        if (isHumanVerified.HasValue)
            records = records.Where(record => record.IsHumanVerified == isHumanVerified.Value);

        if (!string.IsNullOrWhiteSpace(query))
            records = records.Where(record =>
                record.NormalizedSource.Contains(SmartTranslationRequest.Normalize(query)));

        return await records
            .OrderByDescending(record => record.IsHumanVerified)
            .ThenByDescending(record => record.UpdatedAt)
            .Skip(Math.Max(skip, 0))
            .Take(Math.Clamp(take, 1, 100))
            .ToListAsync(cancellationToken);
    }
}
