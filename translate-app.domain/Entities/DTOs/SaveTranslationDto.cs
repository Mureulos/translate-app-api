namespace translate_app.Domain.Entities.DTOs;

public class SaveTranslationDto
{
    public string Text { get; set; } = string.Empty;
    public string TranslationText { get; set; } = string.Empty;
    public int? SourceLanguageId { get; set; }
    public int TargetLanguageId { get; set; }
}

