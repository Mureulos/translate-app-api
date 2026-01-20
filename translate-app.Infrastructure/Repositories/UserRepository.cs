using Microsoft.EntityFrameworkCore;
using translate_app.Domain.Entities;
using translate_app.Domain.Entities.DTOs;
using translate_app.Domain.Repositories;
using translate_app.Infrastructure.Data;

namespace translate_app.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllUsers(CancellationToken cancellationToken = default)
        {
            return await _context.Users
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task<User?> GetUserById(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(user => user.Id == id, cancellationToken);
        }

        public async Task<User> CreateUser(UserDto dto, CancellationToken cancellationToken = default)
        {
            var user = new User
            {
                Name = dto.Name,
                LastName = dto.LastName,
                Email = dto.Email,
                DefaultLanguage = dto.DefaultLanguage,
            };

            await _context.Users.AddAsync(user, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return user;
        }

        public async Task<User?> UpdateUser(int id, UserDto userDto, CancellationToken cancellationToken = default)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == id, cancellationToken);

            if (user is null)
                return null;

            user.Name = userDto.Name;
            user.LastName = userDto.LastName;
            user.Email = userDto.Email;
            user.DefaultLanguage = userDto.DefaultLanguage;

            await _context.SaveChangesAsync(cancellationToken);

            return user;
        }

        public async Task DeleteUser(int id, CancellationToken cancellationToken = default)
        {
            await _context.Users
                .Where(u => u.Id == id)
                .ExecuteDeleteAsync(cancellationToken);
        }
    }
}
