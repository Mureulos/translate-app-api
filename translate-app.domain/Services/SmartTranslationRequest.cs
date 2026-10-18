namespace translate_app.Domain.Services;

public sealed record SmartTranslationRequest(
    string SourceText,
    string TargetLanguage,
    string ContextTag)
{
    public TranslationMemoryIdentity ToIdentity() => new(
        Normalize(SourceText),
        Normalize(TargetLanguage),
        NormalizeContext(ContextTag));

    public static string Normalize(string value) =>
        string.Join(' ', value.Trim().Split((char[]?)null, StringSplitOptions.RemoveEmptyEntries))
            .ToLowerInvariant();

    public static string NormalizeContext(string value) =>
        string.IsNullOrWhiteSpace(value) ? "general" : Normalize(value);
}

public sealed record TranslationMemoryIdentity(
    string NormalizedSource,
    string TargetLanguage,
    string ContextTag);
