namespace translate_app.Application.UseCases.Translation.Translate
{
    public class TranslationRequestDto
    {
        public string Text { get; set; }
        public string TargetLanguage { get; set; }
        public string? SourceLanguage { get; set; }
    }
}