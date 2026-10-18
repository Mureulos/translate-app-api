using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using translate_app.Domain.Services;

namespace translate_app.Infrastructure.Services;

public sealed class RedisTranslationCache(
    IDistributedCache cache,
    IOptions<TranslationCacheOptions> options) : ITranslationCache
{
    public Task<string?> GetAsync(
        TranslationMemoryIdentity identity,
        CancellationToken cancellationToken = default) =>
        cache.GetStringAsync(TranslationCacheKey.Create(identity), cancellationToken);

    public Task SetAsync(
        TranslationMemoryIdentity identity,
        string translation,
        TimeSpan expiration,
        CancellationToken cancellationToken = default) =>
        cache.SetStringAsync(
            TranslationCacheKey.Create(identity),
            translation,
            new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = expiration
            },
            cancellationToken);

    public Task RemoveAsync(
        TranslationMemoryIdentity identity,
        CancellationToken cancellationToken = default) =>
        cache.RemoveAsync(TranslationCacheKey.Create(identity), cancellationToken);

    public TimeSpan GetExpiration() =>
        TimeSpan.FromSeconds(Math.Max(options.Value.TtlSeconds, 1));
}
