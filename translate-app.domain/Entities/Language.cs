using System.ComponentModel.DataAnnotations.Schema;
using translate_app.Domain.Abstractions;

namespace translate_app.Domain.Entities
{
    public class Language: Entity, IAgregateRoot
    {
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;

        [NotMapped]
        public string LocalizedName { get; set; } = string.Empty;
    }
}
