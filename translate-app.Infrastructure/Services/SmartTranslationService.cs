using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Npgsql;
using translate_app.Domain.Repositories;
using translate_app.Domain.Services;

namespace translate_app.Infrastructure.Services;

public sealed class SmartTranslationService(
    ITranslationCache cache,
    ITranslationRecordRepository translationRecordRepository,
    IAIService aiService,
    IOptions<TranslationCacheOptions> cacheOptions) : ISmartTranslationService
{
    public async Task<string> TranslateAsync(
        SmartTranslationRequest request,
        string? sourceLanguage = null,
        CancellationToken cancellationToken = default)
    {
        var identity = request.ToIdentity();
        var cachedTranslation = await cache.GetAsync(identity, cancellationToken);
        if (cachedTranslation is not null)
            return cachedTranslation;

        var storedRecord = await translationRecordRepository.GetByIdentityAsync(
            identity,
            cancellationToken);

        if (storedRecord is not null)
        {
            await CacheAsync(identity, storedRecord.TranslatedText, cancellationToken);
            return storedRecord.TranslatedText;
        }

        var translatedText = await aiService.TranslateAsync(
            request.SourceText,
            request.TargetLanguage,
            sourceLanguage,
            cancellationToken,
            request.ContextTag);

        if (string.IsNullOrWhiteSpace(translatedText))
            throw new InvalidOperationException("The translation provider returned an empty result.");

        try
        {
            await translationRecordRepository.CreateAutomaticAsync(
                identity,
                translatedText,
                cancellationToken);
        }
        catch (DbUpdateException exception) when (IsUniqueViolation(exception))
        {
            storedRecord = await translationRecordRepository.GetByIdentityAsync(
                identity,
                cancellationToken);

            if (storedRecord is null)
                throw;

            translatedText = storedRecord.TranslatedText;
        }

        await CacheAsync(identity, translatedText, cancellationToken);
        return translatedText;
    }

    public Task InvalidateCacheAsync(
        SmartTranslationRequest request,
        CancellationToken cancellationToken = default) =>
        cache.RemoveAsync(request.ToIdentity(), cancellationToken);

    private Task CacheAsync(
        TranslationMemoryIdentity identity,
        string translatedText,
        CancellationToken cancellationToken) =>
        cache.SetAsync(
            identity,
            translatedText,
            TimeSpan.FromSeconds(Math.Max(cacheOptions.Value.TtlSeconds, 1)),
            cancellationToken);

    private static bool IsUniqueViolation(DbUpdateException exception) =>
        exception.InnerException is PostgresException
        {
            SqlState: PostgresErrorCodes.UniqueViolation
        };
}
