using translate_app.Domain.Abstractions;

namespace translate_app.Domain.Entities;

public class TranslationRecord : IAgregateRoot
{
    public int Id { get; set; }
    public string NormalizedSource { get; set; } = string.Empty;
    public string TargetLanguage { get; set; } = string.Empty;
    public string ContextTag { get; set; } = string.Empty;
    public string TranslatedText { get; set; } = string.Empty;
    public bool IsHumanVerified { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}