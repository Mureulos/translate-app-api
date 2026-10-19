namespace translate_app.Application.UseCases.TranslationMemory.Models;

public class TranslationMemoryItemResponse
{
    public int Id { get; init; }
    public string NormalizedSource { get; init; } = string.Empty;
    public string TargetLanguage { get; init; } = string.Empty;
    public string ContextTag { get; init; } = string.Empty;
    public string TranslatedText { get; init; } = string.Empty;
    public bool IsHumanVerified { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime UpdatedAt { get; init; }
}
