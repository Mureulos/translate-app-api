using Azure;
using translate_app.Application.UseCases.User.Entities.Response;
using translate_app.Application.UseCases.User.GetUserById;
using translate_app.Domain.Abstractions;
using translate_app.Domain.Repositories;

namespace translate_app.Application.UseCases.User.GetUserByEmail
{
    public sealed class Handler
    {
        private readonly IUserRepository _userRepository;
        public Handler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result<Response>> Handle(GetUserByEmailQuery command, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (command.UserEmail == null)
                return Result.Failure<Response>(new Error("400", "User id is required"));

            var user = await _userRepository.GetUserByEmail(command.UserEmail, cancellationToken);

            if (user == null)
                return Result.Failure<Response>(new Error("404", "User not found"));

            var userResponse = new UserResponse
            {
                Id = user.Id,
                Name = user.Name,
                LastName = user.LastName,
                Email = user.Email,
                Password = user.Password,
                Role = user.Role,
                DefaultLanguage = user.DefaultLanguage,
                CreateAt = user.CreateAt
            };

            return Result.Success(new Response(userResponse));
        }
    }
}
