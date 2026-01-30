using MediatR;
using translate_app.Domain.Abstractions;
using translate_app.Domain.Entities.DTOs;

namespace translate_app.Application.UseCases.User.UpdateUser;

public sealed record UpdateUserCommand(int Id, UserDto Dto) : IRequest<Result<Response>>;
