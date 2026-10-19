using MediatR;
using translate_app.Domain.Abstractions;

namespace translate_app.Application.UseCases.TranslationMemory.SearchTranslationMemory;

public sealed record SearchTranslationMemoryQuery(
    string? TargetLanguage,
    string? ContextTag,
    bool? IsHumanVerified,
    string? Query,
    int Skip = 0,
    int Take = 50) : IRequest<Result<Response>>;
