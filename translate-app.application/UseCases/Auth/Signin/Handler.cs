using MediatR;
using translate_app.Application.UseCases.User.CreateUser;
using translate_app.Application.UseCases.User.Entities.Response;
using translate_app.Domain.Abstractions;
using translate_app.Domain.Entities.DTOs;
using translate_app.Domain.Repositories;
using translate_app.Infrastructure.Repositories;
using translate_app.Infrastructure.Services;

namespace translate_app.Application.UseCases.Auth.Signin
{
    public sealed class Handler : IRequestHandler<SiginCommand, Result<Response>>
    {
        private readonly IUserRepository _userRepository;
        private readonly PasswordHasherService _passwordHasher;
        private readonly ILanguageRepository _languageRepository;

        public Handler(IUserRepository userRepository, PasswordHasherService passwordHasher, ILanguageRepository languageRepository)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _languageRepository = languageRepository;
        }

        public async Task<Result<Response>> Handle(SiginCommand command, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (command.Dto is null)
                return Result.Failure<Response>(new Error("400", "Data not found"));

            if (!string.IsNullOrWhiteSpace(command.Dto.Password))
                command.Dto.Password = _passwordHasher.Hash(command.Dto.Password);
            else
                command.Dto.Password = "";
            
            int defaultLanguage = _languageRepository.GetLanguageById(command.Dto.DefaultLanguage, cancellationToken).Result?.Id ?? 0;
            
            if (defaultLanguage == 0)
                return Result.Failure<Response>(new Error("400", "Language not found"));

            var dto = new UserDto
            {
                Name = command.Dto.Name,
                LastName = command.Dto.LastName,
                Email = command.Dto.Email,
                DefaultLanguage = defaultLanguage
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
                DefaultLanguage = defaultLanguage,
                CreateAt = DateTime.Now
            };

            return Result.Success(new Response(userResponse));
        }
    }
}
