using MediatR;
using translate_app.Domain.Abstractions;

namespace translate_app.Application.UseCases.User.DeleteUser
{
    public sealed record DeleteUserCommand(int UserId) : IRequest<Result<Response>>;
}
