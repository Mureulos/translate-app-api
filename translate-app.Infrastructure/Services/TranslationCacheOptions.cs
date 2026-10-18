namespace translate_app.Infrastructure.Services;

public sealed class TranslationCacheOptions
{
    public const string SectionName = "TranslationCache";

    public int TtlSeconds { get; set; } = 3600;
}
