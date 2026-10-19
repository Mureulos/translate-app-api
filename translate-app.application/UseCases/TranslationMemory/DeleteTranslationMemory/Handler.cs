using MediatR;
using translate_app.Domain.Abstractions;
using translate_app.Domain.Repositories;
using translate_app.Domain.Services;

namespace translate_app.Application.UseCases.TranslationMemory.DeleteTranslationMemory;

public sealed class Handler(
    ITranslationRecordRepository translationRecordRepository,
    ISmartTranslationService smartTranslationService)
    : IRequestHandler<DeleteTranslationMemoryCommand, Result<Response>>
{
    public async Task<Result<Response>> Handle(DeleteTranslationMemoryCommand command, CancellationToken cancellationToken)
    {
        var current = await translationRecordRepository.GetByIdAsync(command.Id, cancellationToken);
        if (current is null)
            return Result.Failure<Response>(new Error("TranslationMemory.NotFound", "Translation memory entry not found", ErrorType.NotFound));

        await translationRecordRepository.DeleteAsync(command.Id, cancellationToken);

        await smartTranslationService.InvalidateCacheAsync(
            new SmartTranslationRequest(current.NormalizedSource, current.TargetLanguage, current.ContextTag),
            cancellationToken);

        return Result.Success(new Response());
    }
}
