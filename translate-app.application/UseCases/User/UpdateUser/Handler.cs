using MediatR;
using translate_app.Application.UseCases.User.Entities.Response;
using translate_app.Domain.Abstractions;
using translate_app.Domain.Repositories;

namespace translate_app.Application.UseCases.User.UpdateUser;

public sealed class Handler : IRequestHandler<UpdateUserCommand, Result<Response>>
{
    private readonly IUserRepository _userRepository;

    public Handler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result<Response>> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (command.dto is null)
            return Result.Failure<Response>(new Error("400", "Data not found"));

        var user = await _userRepository.UpdateUser(command.id, command.dto, cancellationToken);

        if (user is null)
            return Result.Failure<Response>(new Error("404", "User not found"));

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
