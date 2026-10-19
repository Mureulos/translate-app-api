namespace translate_app.Application.UseCases.TranslationMemory.Models;

public class TranslationMemoryDto
{
    public string SourceText { get; set; } = string.Empty;
    public string TargetLanguage { get; set; } = string.Empty;
    public string ContextTag { get; set; } = "general";
    public string TranslatedText { get; set; } = string.Empty;
}
