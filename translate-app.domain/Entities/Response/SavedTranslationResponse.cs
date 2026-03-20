using translate_app.Domain.Abstractions;

namespace translate_app.Domain.Entities.Response;

public class SavedTranslationResponse: Entity
{
    public string Text { get; set; } = String.Empty;
    public string TranslationText { get; set; } = String.Empty;
    public int UserId { get; set; }
    public DateTime CreatedAt { get; set; }
}