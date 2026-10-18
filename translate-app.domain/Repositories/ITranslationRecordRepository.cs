using translate_app.Domain.Entities;
using translate_app.Domain.Services;

namespace translate_app.Domain.Repositories;

public interface ITranslationRecordRepository
{
    Task<TranslationRecord?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken = default);

    Task<TranslationRecord?> GetByIdentityAsync(
        TranslationMemoryIdentity identity,
        CancellationToken cancellationToken = default);

    Task<TranslationRecord> CreateHumanVerifiedAsync(
        TranslationMemoryIdentity identity,
        string translatedText,
        CancellationToken cancellationToken = default);

    Task<TranslationRecord> CreateAutomaticAsync(
        TranslationMemoryIdentity identity,
        string translatedText,
        CancellationToken cancellationToken = default);

    Task<TranslationRecord?> UpdateHumanVerifiedAsync(
        int id,
        TranslationMemoryIdentity identity,
        string translatedText,
        CancellationToken cancellationToken = default);

    Task DeleteAsync(int id, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<TranslationRecord>> SearchAsync(
        string? targetLanguage,
        string? contextTag,
        bool? isHumanVerified,
        string? query,
        int skip,
        int take,
        CancellationToken cancellationToken = default);
}
