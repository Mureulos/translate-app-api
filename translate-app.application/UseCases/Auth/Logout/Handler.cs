using MediatR;
using translate_app.Domain.Abstractions;

namespace translate_app.Application.UseCases.Auth.Logout
{
    public sealed class Handler : IRequestHandler<LogoutCommand, Result<Response>>
    {
        public async Task<Result<Response>> Handle(LogoutCommand command, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            return Result<Response>.Success(new Response());
        }
    }
}
