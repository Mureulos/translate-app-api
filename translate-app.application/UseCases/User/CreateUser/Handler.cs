using MediatR;
using Microsoft.AspNetCore.Identity;
using translate_app.Application.UseCases.User.Entities.Response;
using translate_app.Domain.Abstractions;
using translate_app.Domain.Entities.DTOs;
using translate_app.Domain.Repositories;
using translate_app.Infrastructure.Services;

namespace translate_app.Application.UseCases.User.CreateUser
{
    public sealed class Handler : IRequestHandler<CreateUserCommand, Result<Response>>
    {
        private readonly IUserRepository _userRepository;
        private readonly PasswordHasherService _passwordHasher;

        public Handler(IUserRepository userRepository, PasswordHasherService passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;   
        }

        public async Task<Result<Response>> Handle(CreateUserCommand command, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (command.Dto is null)
                return Result.Failure<Response>(new Error("400", "Data not found"));

            if (!string.IsNullOrWhiteSpace(command.Dto.Password))
                command.Dto.Password = _passwordHasher.Hash(command.Dto.Password);
            else
                command.Dto.Password = "";

            var dto = new UserDto
            {
                Name = command.Dto.Name,
                LastName = command.Dto.LastName,
                Email = command.Dto.Email,
                DefaultLanguage = command.Dto.DefaultLanguage
            };

            var user = await _userRepository.CreateUser(command.Dto, cancellationToken);

            if (user is null)
                return Result.Failure<Response>(new Error("404", "User not found"));

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
