namespace translate_app.Application.UseCases.Translation.Entities.DTOs
{
    public class TranslationDto
    {
        public string Text { get; set; } = string.Empty;
        public int TargetLanguageId { get; set; }
        public int? SourceLanguageId { get; set; }
    }
}