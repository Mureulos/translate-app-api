using translate_app.Domain.Entities;
using translate_app.Domain.Entities.DTOs;

namespace translate_app.Domain.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<IEnumerable<User>> GetAllUsers(CancellationToken cancellationToken);
        Task<User?> GetUserById(int id, CancellationToken cancellationToken);
        Task<User?> GetUserByEmail(string email, CancellationToken cancellationToken);
        Task<User> CreateUser(UserDto user, CancellationToken cancellationToken);
        Task<User> UpdateUser(int id, UserDto user, CancellationToken cancellationToken);
        Task DeleteUser(int id, CancellationToken cancellaionToken);
    }
}
