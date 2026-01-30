using MediatR;
using translate_app.Domain.Abstractions;

namespace translate_app.Application.UseCases.Auth.Logout
{
    public sealed record LogoutCommand: IRequest<Result<Response>>;
}
