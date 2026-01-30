using MediatR;
using translate_app.Domain.Abstractions;

namespace translate_app.Application.UseCases.User.GetUserById
{
    public sealed record GetUserByIdQuery(int UserId) : IRequest<Result<Response>>;
}
