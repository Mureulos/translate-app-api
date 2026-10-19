using MediatR;
using translate_app.Domain.Abstractions;

namespace translate_app.Application.UseCases.TranslationMemory.GetTranslationMemoryById;

public sealed record GetTranslationMemoryByIdQuery(int Id) : IRequest<Result<Response>>;
