using MediatR;
using translate_app.Application.UseCases.TranslationMemory.Models;
using translate_app.Domain.Abstractions;
using translate_app.Domain.Repositories;

namespace translate_app.Application.UseCases.TranslationMemory.SearchTranslationMemory;

public sealed class Handler(ITranslationRecordRepository translationRecordRepository)
    : IRequestHandler<SearchTranslationMemoryQuery, Result<Response>>
{
    public async Task<Result<Response>> Handle(SearchTranslationMemoryQuery query, CancellationToken cancellationToken)
    {
        var rows = await translationRecordRepository.SearchAsync(
            query.TargetLanguage,
            query.ContextTag,
            query.IsHumanVerified,
            query.Query,
            query.Skip,
            query.Take,
            cancellationToken);

        return Result.Success(new Response(rows.Select(x => x.ToResponse()).ToArray()));
    }
}
