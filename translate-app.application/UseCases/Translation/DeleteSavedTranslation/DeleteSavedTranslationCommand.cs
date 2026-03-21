using MediatR;
using translate_app.Domain.Abstractions;

namespace translate_app.application.UseCases.Translation.DeleteSavedTranslation;

public sealed record DeleteSavedTranslationCommand(int savedTranslationId) : IRequest<Result<Response>>;