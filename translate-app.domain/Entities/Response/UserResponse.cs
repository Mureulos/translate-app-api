using translate_app.Domain.Abstractions;

namespace translate_app.Application.UseCases.User.Entities.Response
{
    public class UserResponse: Entity, IAgregateRoot
    {
        public string Name { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string DefaultLanguage { get; set; } = string.Empty;
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
    }
}
