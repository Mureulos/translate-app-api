using System.Text.Json.Serialization;
using translate_app.Domain.Abstractions;

namespace translate_app.Domain.Entities.DTOs
{
    public class UserDto : Entity, IAgregateRoot
    {
        public string Name { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string DefaultLanguage { get; set; } = string.Empty;
    }
}
