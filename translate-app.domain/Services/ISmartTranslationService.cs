namespace translate_app.Domain.Services;

public interface ISmartTranslationService
{
    Task InvalidateCacheAsync(
        SmartTranslationRequest request,
        CancellationToken cancellationToken = default);
}