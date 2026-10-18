namespace translate_app.Domain.Services;

public sealed record SmartTranslationRequest(
    string SourceText,
    string TargetLanguage,
    string ContextTag)
{
    public TranslationMemoryIdentity ToIdentity() => new(
        Normalize(SourceText),
        Normalize(TargetLanguage),
        Normalize(ContextTag));

    private static string Normalize(string value) =>
        string.Join(' ', value.Trim().Split((char[]?)null, StringSplitOptions.RemoveEmptyEntries))
            .ToLowerInvariant();
}

public sealed record TranslationMemoryIdentity(
    string NormalizedSource,
    string TargetLanguage,
    string ContextTag);
