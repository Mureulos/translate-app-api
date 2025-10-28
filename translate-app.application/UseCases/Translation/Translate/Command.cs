using MediatR;
using translate_app.Domain.Abstractions;

namespace translate_app.Application.UseCases.Translation.Translate
{
    public sealed record Command(TranslationRequestDto Request) : IRequest<Result<Response>>;
}
