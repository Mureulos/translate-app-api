using MediatR;
using translate_app.Application.UseCases.User.Entities.Response;
using translate_app.Domain.Abstractions;
using translate_app.Domain.Entities.DTOs;
using translate_app.Domain.Repositories;

namespace translate_app.Application.UseCases.User.CreateUser
{
    public sealed class Handler : IRequestHandler<CreateUserCommand, Result<Response>>
    {
        private readonly IUserRepository _userRepository;

        public Handler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result<Response>> Handle(CreateUserCommand command, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (command.dto is null)
                return Result.Failure<Response>(new Error("400", "Data not found"));

            var dto = new UserDto
            {
                Name = command.dto.Name,
                LastName = command.dto.LastName,
                Email = command.dto.Email,
                DefaultLanguage = command.dto.DefaultLanguage
            };

            var user = await _userRepository.CreateUser(command.dto, cancellationToken);

            if (user is null)
                return Result.Failure<Response>(new Error("500", "Can't create the user"));

            var userResponse = new UserResponse
            {
                Id = user.Id,
                Name = user.Name,
                LastName = user.LastName,
                Email = user.Email,
                DefaultLanguage = user.DefaultLanguage,
                CreateAt = DateTime.Now
            };

            return Result.Success(new Response(userResponse));
        }
    }
}
