using MediatR;
using translate_app.Application.UseCases.TranslationMemory.Models;
using translate_app.Domain.Abstractions;

namespace translate_app.Application.UseCases.TranslationMemory.CreateTranslationMemory;

public sealed record CreateTranslationMemoryCommand(TranslationMemoryDto Dto) : IRequest<Result<Response>>;
