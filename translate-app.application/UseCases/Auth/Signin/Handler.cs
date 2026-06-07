using MediatR;
using translate_app.Domain.Abstractions;
using translate_app.Domain.Entities.DTOs;
using translate_app.Domain.Entities.Response;
using translate_app.Domain.Repositories;
using translate_app.Infrastructure.Services;

namespace translate_app.Application.UseCases.Auth.Signin
{
    public sealed class Handler : IRequestHandler<SiginCommand, Result<Response>>
    {
        private readonly IUserRepository _userRepository;
        private readonly PasswordHasherService _passwordHasher;
        private readonly ILanguageRepository _languageRepository;
        private readonly TokenService _tokenService;

        public Handler(IUserRepository userRepository, PasswordHasherService passwordHasher,
            ILanguageRepository languageRepository, TokenService tokenService)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _languageRepository = languageRepository;
            _tokenService = tokenService;
        }

        public async Task<Result<Response>> Handle(SiginCommand command, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (command.Dto is null)
                return Result.Failure<Response>(new Error("Sigin.DataMissing", "Data missing", ErrorType.Validation));

            if (string.IsNullOrWhiteSpace(command.Dto.Email))
                return Result.Failure<Response>(new Error("Auth.EmailMissing", "Email is required",
                    ErrorType.Validation));

            if (string.IsNullOrWhiteSpace(command.Dto.Password))
                return Result.Failure<Response>(new Error("Auth.PasswordMissing", "Password is required",
                    ErrorType.Validation));

            command.Dto.Email = command.Dto.Email.Trim().ToLower();

            var existingUser = await _userRepository.GetUserByEmail(command.Dto.Email, cancellationToken);

            if (existingUser is not null)
                return Result.Failure<Response>(new Error("Auth.EmailConflict", "This email is already in use",
                    ErrorType.Conflict));

            command.Dto.Password = _passwordHasher.Hash(command.Dto.Password);

            var language = await _languageRepository.GetLanguageById(command.Dto.DefaultLanguage, cancellationToken);

            if (language is null)
                return Result.Failure<Response>(new Error("Language.NotFound", "Default language not found",
                    ErrorType.NotFound));

            var user = await _userRepository.CreateUser(command.Dto, cancellationToken);

            if (user is null)
                return Result.Failure<Response>(new Error("User.CreationFailed", "Failed to create user",
                    ErrorType.Failure));

            var token = _tokenService.Create(user);

            var authResponse = new AuthResponse(
                Token: token,
                Email: user.Email,
                UserName: user.Name
            );

            return Result<Response>.Success(new Response(authResponse));
        }
    }
}