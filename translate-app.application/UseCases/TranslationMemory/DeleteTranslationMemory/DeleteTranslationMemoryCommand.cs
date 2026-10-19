using MediatR;
using translate_app.Domain.Abstractions;

namespace translate_app.Application.UseCases.TranslationMemory.DeleteTranslationMemory;

public sealed record DeleteTranslationMemoryCommand(int Id) : IRequest<Result<Response>>;
