using MediatR;
using translate_app.Application.UseCases.User.Entities.Response;
using translate_app.Domain.Abstractions;
using translate_app.Domain.Repositories;

namespace translate_app.Application.UseCases.User.GetUserById
{
    public sealed class Handler : IRequestHandler<GetUserByIdQuery, Result<Response>>
    {
        private readonly IUserRepository _userRepository;

        public Handler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result<Response>> Handle(GetUserByIdQuery command, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (command.UserId <= 0)
                return Result.Failure<Response>(new Error("User.MissingData", "User id not found", ErrorType.Validation));

            var user = await _userRepository.GetUserById(command.UserId, cancellationToken);

            if (user == null)
                return Result.Failure<Response>(new Error("User.NotFound", "User not found", ErrorType.NotFound));

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
