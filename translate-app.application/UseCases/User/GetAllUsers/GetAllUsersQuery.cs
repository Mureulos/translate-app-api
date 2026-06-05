using MediatR;
using translate_app.Domain.Abstractions;

namespace translate_app.Application.UseCases.User.GetAllUsers
{
    public sealed record GetAllUsersQuery: IRequest<Result<Response>>;
}
