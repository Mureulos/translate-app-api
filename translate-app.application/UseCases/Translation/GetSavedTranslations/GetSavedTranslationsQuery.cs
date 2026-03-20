using MediatR;
using translate_app.Domain.Abstractions;

namespace translate_app.application.UseCases.Translation.GetSavedTranslations;

public sealed record GetSavedTranslationsQuery(int userId) : IRequest<Result<Response>>;