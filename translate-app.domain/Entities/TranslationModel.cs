using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using translate_app.Domain.Abstractions;

namespace translate_app.domain.Models
{
    public class TranslationModel: Entity
    {
        public string OriginalText { get; set; } = string.Empty;
        public string TargetLanguage { get; set; } = string.Empty;

        public int CharacterCount => OriginalText.Length;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    }
}
