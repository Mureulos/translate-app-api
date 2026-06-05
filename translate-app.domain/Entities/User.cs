using translate_app.Domain.Abstractions;

namespace translate_app.Domain.Entities
{
    public class User: Entity, IAgregateRoot
    {
        public string Name { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public int DefaultLanguage { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
    }
}
