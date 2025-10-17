using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using translate_app.Domain.Abstractions;

namespace translate_app.domain.Models
{
    public class TranslationRequest: Entity, IAgregateRoot
    {
        public string Text { get; set; } = string.Empty;
        public string? SourceLanguage { get; set; }
        public string TargetLanguage { get; set; } = string.Empty;
        public string Translation { get; set; } = string.Empty;

        public int CharacterCount => Text.Length;
        public int? UserId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    }
}
