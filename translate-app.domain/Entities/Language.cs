using translate_app.Domain.Abstractions;

namespace translate_app.Domain.Entities
{
    public class Language: Entity, IAgregateRoot
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string LocalizedName { get; set; }
    }
}
