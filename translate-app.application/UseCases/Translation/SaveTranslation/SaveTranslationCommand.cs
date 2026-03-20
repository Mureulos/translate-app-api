using MediatR;
using translate_app.Domain.Abstractions;
using translate_app.Domain.Entities.DTOs;

namespace translate_app.application.UseCases.Translation.SaveTranslation;

public sealed record SaveTranslationCommand(SaveTranslationDto Dto, int UserId) : IRequest<Result<Response>>;
