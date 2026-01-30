using MediatR;
using translate_app.Application.UseCases.User.Entities.Response;
using translate_app.Domain.Abstractions;
using translate_app.Domain.Repositories;

namespace translate_app.Application.UseCases.User.GetAllUsers
{
    public sealed class Handler : IRequestHandler<GetAllUsersQuery, Result<Response>>
    {
        private readonly IUserRepository _userRepository;

        public Handler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result<Response>> Handle(GetAllUsersQuery command, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var users = await _userRepository.GetAllUsers(cancellationToken);

            if (users is null || !users.Any())
                return Result.Failure<Response>(new Error("400", "User not found"));

            var usersResponse = users
                .Select(u => new UserResponse
                {
                    Id = u.Id,
                    Name = u.Name,
                    LastName = u.LastName,
                    Email = u.Email,
                    Password = u.Password,
                    Role = u.Role,
                    DefaultLanguage = u.DefaultLanguage,
                    CreateAt = u.CreateAt
                })
                .ToArray();

            return Result.Success(new Response(usersResponse));
        }
    }
}
