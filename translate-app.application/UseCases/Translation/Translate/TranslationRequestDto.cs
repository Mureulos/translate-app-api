namespace translate_app.Application.UseCases.Translation.Translate
{
    public class TranslationRequestDto
    {
        public string Text { get; set; } = string.Empty;
        public int TargetLanguageId { get; set; }
        public int? SourceLanguageId { get; set; }
    }
}