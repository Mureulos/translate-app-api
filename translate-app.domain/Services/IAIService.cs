namespace translate_app.Domain.Services;

public interface IAIService
{
    Task<string> TranslateAsync(
        string text,
        string targetLanguage,
        string? sourceLanguage = null,
        CancellationToken cancellationToken = default);
}