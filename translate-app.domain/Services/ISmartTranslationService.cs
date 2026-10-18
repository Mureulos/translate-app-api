namespace translate_app.Domain.Services;

public interface ISmartTranslationService
{
    Task<string> TranslateAsync(
        SmartTranslationRequest request,
        string? sourceLanguage = null,
        CancellationToken cancellationToken = default);

    Task InvalidateCacheAsync(
        SmartTranslationRequest request,
        CancellationToken cancellationToken = default);
}

public interface ITranslationCache
{
    Task<string?> GetAsync(
        TranslationMemoryIdentity identity,
        CancellationToken cancellationToken = default);

    Task SetAsync(
        TranslationMemoryIdentity identity,
        string translation,
        TimeSpan expiration,
        CancellationToken cancellationToken = default);

    Task RemoveAsync(
        TranslationMemoryIdentity identity,
        CancellationToken cancellationToken = default);
}