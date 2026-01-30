using MediatR;
using translate_app.Domain.Abstractions;
using translate_app.Domain.Entities.DTOs;
using translate_app.Domain.Entities.Response;

namespace translate_app.Application.UseCases.Auth.Login
{
    public sealed record LoginCommand(AuthDto Dto): IRequest<Result<AuthResponse>>;
}
