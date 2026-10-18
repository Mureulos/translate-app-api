using translate_app.Domain.Services;

namespace translate_app.Infrastructure.Services;

public static class TranslationCacheKey
{
    public static string Create(TranslationMemoryIdentity identity) =>
        $"trans:{Escape(identity.TargetLanguage)}:{Escape(identity.ContextTag)}:{Escape(identity.NormalizedSource)}";

    private static string Escape(string value) =>
        value.Replace(":", "%3A", StringComparison.Ordinal);
}
