using translate_app.Domain.Abstractions;

namespace translate_app.Domain.Entities
{
    public class TranslationR : Entity, IAgregateRoot
    {
        public string Text { get; set; } = string.Empty;
        public string TranslationText { get; set; } = string.Empty;

        public int? SourceLanguageId { get; set; }
        public Language? SourceLanguage { get; set; }

        public int TargetLanguageId { get; set; }
        public Language TargetLanguage { get; set; }

        public int CharacterCount => Text.Length;
        public int? UserId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
