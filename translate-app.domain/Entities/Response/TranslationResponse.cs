using translate_app.Domain.Entities;

namespace translate_app.Application.UseCases.Translation.Entities.Response;

public class TranslationResponse
{
    public int Id { get; init; }
    public required string Text { get; init; }
    public required string Translation { get; init; }
    public Language? SourceLanguage { get; init; }
    public required Language TargetLanguage { get; init; }
    public int CharacterCount { get; init; }
    public DateTime CreatedAt { get; init; }
}