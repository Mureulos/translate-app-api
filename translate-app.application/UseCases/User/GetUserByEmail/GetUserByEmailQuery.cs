using MediatR;
using translate_app.Domain.Abstractions;

namespace translate_app.Application.UseCases.User.GetUserByEmail
{
    public sealed record GetUserByEmailQuery(string UserEmail): IRequest<Result<Response>>;
}
