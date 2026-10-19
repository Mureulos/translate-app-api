using MediatR;
using translate_app.Application.UseCases.TranslationMemory.Models;
using translate_app.Domain.Abstractions;
using translate_app.Domain.Repositories;

namespace translate_app.Application.UseCases.TranslationMemory.GetTranslationMemoryById;

public sealed class Handler(ITranslationRecordRepository translationRecordRepository)
    : IRequestHandler<GetTranslationMemoryByIdQuery, Result<Response>>
{
    public async Task<Result<Response>> Handle(GetTranslationMemoryByIdQuery query, CancellationToken cancellationToken)
    {
        var current = await translationRecordRepository.GetByIdAsync(query.Id, cancellationToken);
        if (current is null)
            return Result.Failure<Response>(new Error("TranslationMemory.NotFound", "Translation memory entry not found", ErrorType.NotFound));

        return Result.Success(new Response(current.ToResponse()));
    }
}
