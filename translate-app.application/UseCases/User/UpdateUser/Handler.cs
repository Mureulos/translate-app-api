using MediatR;
using translate_app.Application.UseCases.User.Entities.Response;
using translate_app.Domain.Abstractions;
using translate_app.Domain.Repositories;
using translate_app.Infrastructure.Services;

namespace translate_app.Application.UseCases.User.UpdateUser;

public sealed class Handler : IRequestHandler<UpdateUserCommand, Result<Response>>
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

    public async Task<Result<Response>> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (command.Dto is null)
            return Result.Failure<Response>(new Error("User.MissingData", "Data not found", ErrorType.Validation));

        var language = await _languageRepository.GetLanguageById(command.Dto.DefaultLanguage, cancellationToken);
        
        if (language is null)
            return Result.Failure<Response>(new Error("Language.NotFound", "Default language not found", ErrorType.NotFound));

        if (!string.IsNullOrWhiteSpace(command.Dto.Password))
            command.Dto.Password = _passwordHasher.Hash(command.Dto.Password);
        else
            command.Dto.Password = "";

        var user = await _userRepository.UpdateUser(command.Id, command.Dto, cancellationToken);

        if (user is null)
            return Result.Failure<Response>(new Error("User.NotFound", "User id not found", ErrorType.NotFound));

        var userResponse = new UserResponse
        {
            Id = user.Id,
            Name = user.Name,
            LastName = user.LastName,
            Email = user.Email,
            Role = user.Role,
            DefaultLanguage = user.DefaultLanguage,
            CreateAt = user.CreateAt
        };

        return Result.Success(new Response(userResponse));
    }
}