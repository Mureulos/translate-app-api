using MediatR;
using translate_app.Domain.Abstractions;

namespace translate_app.Application.UseCases.Languages.GetLanguageById
{
    public sealed record GetLanguageByIdQuery(int idLanguage) : IRequest<Result<Response>>;
}
