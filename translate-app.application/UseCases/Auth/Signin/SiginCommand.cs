using MediatR;
using translate_app.Domain.Abstractions;
using translate_app.Domain.Entities.DTOs;

namespace translate_app.Application.UseCases.Auth.Signin
{
    public sealed record SiginCommand(UserDto Dto) : IRequest<Result<Response>>;
}
