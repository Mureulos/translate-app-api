using MediatR;
using translate_app.Domain.Abstractions;
using translate_app.Domain.Entities.DTOs;

namespace translate_app.Application.UseCases.User.CreateUser
{
    public sealed record CreateUserCommand(UserDto dto) : IRequest<Result<Response>>;
}
