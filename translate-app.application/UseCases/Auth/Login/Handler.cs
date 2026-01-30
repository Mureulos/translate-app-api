using MediatR;
using translate_app.Domain.Abstractions;
using translate_app.Domain.Entities.Response;
using translate_app.Domain.Repositories;
using translate_app.Infrastructure.Services;

namespace translate_app.Application.UseCases.Auth.Login
{
    public sealed class Handler : IRequestHandler<LoginCommand, Result<AuthResponse>>
    {
        private readonly IUserRepository _userRepository;
        private readonly TokenService _tokenService;
        private readonly PasswordHasherService _passwordHasher;

        public Handler(
            IUserRepository userRepository,
            TokenService tokenService,
            PasswordHasherService passwordHasher)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
            _passwordHasher = passwordHasher;
        }

        public async Task<Result<AuthResponse>> Handle(LoginCommand command, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByEmail(command.Dto.Email, cancellationToken);

            if (user is null)
            {
                return Result.Failure<AuthResponse>(Error.InvalidCredentials);
            }

            bool verified = _passwordHasher.Verify(command.Dto.Password, user.Password);

            if (!verified)
            {
                return Result.Failure<AuthResponse>(Error.InvalidCredentials);
            }

            var token = _tokenService.Create(user);

            var response = new AuthResponse(
                Token: token,
                Email: user.Email,
                UserName: user.Name
            );

            return Result<AuthResponse>.Success(response);
        }
    }
}