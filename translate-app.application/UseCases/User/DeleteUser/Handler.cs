using MediatR;
using translate_app.Application.UseCases.User.Entities.Response;
using translate_app.Domain.Abstractions;
using translate_app.Domain.Repositories;

namespace translate_app.Application.UseCases.User.DeleteUser
{
    public sealed class Handler : IRequestHandler<DeleteUserCommand, Result<Response>>
    {
        private readonly IUserRepository _userRepository;

        public Handler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result<Response>> Handle(DeleteUserCommand command, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (command.userId == null)
                return Result.Failure<Response>(new Error("400", "User id is required"));

            await _userRepository.DeleteUser(command.userId, cancellationToken);

            return Result.Success(new Response());
        }
    }
}
