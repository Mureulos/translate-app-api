using MediatR;
using translate_app.Domain.Abstractions;

namespace translate_app.Application.UseCases.Languages.GetAllLanguages
{
    public sealed record Command: IRequest<Result<Response>>;
}
