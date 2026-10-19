using MediatR;
using translate_app.Application.UseCases.TranslationMemory.Models;
using translate_app.Domain.Abstractions;

namespace translate_app.Application.UseCases.TranslationMemory.UpdateTranslationMemory;

public sealed record UpdateTranslationMemoryCommand(int Id, TranslationMemoryDto Dto) : IRequest<Result<Response>>;
