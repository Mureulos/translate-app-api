using translate_app.Domain.Entities;

namespace translate_app.Application.UseCases.TranslationMemory.Models;

public static class TranslationMemoryMapper
{
    public static TranslationMemoryItemResponse ToResponse(this TranslationRecord record) => new()
    {
        Id = record.Id,
        NormalizedSource = record.NormalizedSource,
        TargetLanguage = record.TargetLanguage,
        ContextTag = record.ContextTag,
        TranslatedText = record.TranslatedText,
        IsHumanVerified = record.IsHumanVerified,
        CreatedAt = record.CreatedAt,
        UpdatedAt = record.UpdatedAt
    };
}
