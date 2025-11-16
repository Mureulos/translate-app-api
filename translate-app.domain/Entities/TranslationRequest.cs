using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using translate_app.Domain.Abstractions;
using translate_app.Domain.Entities;

namespace translate_app.Domain.Entities
{
    public class TranslationResult : Entity, IAgregateRoot
    {
        public string Text { get; set; } = string.Empty;
        public string Translation { get; set; } = string.Empty;

        public int? SourceLanguageId { get; set; }
        public Language? SourceLanguage { get; set; }

        public int TargetLanguageId { get; set; }
        public Language TargetLanguage { get; set; }

        public int CharacterCount => Text.Length;
        public int? UserId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
