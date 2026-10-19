using MediatR;
using translate_app.Application.UseCases.TranslationMemory.Models;
using translate_app.Domain.Abstractions;
using translate_app.Domain.Repositories;
using translate_app.Domain.Services;

namespace translate_app.Application.UseCases.TranslationMemory.UpdateTranslationMemory;

public sealed class Handler(
    ITranslationRecordRepository translationRecordRepository,
    ISmartTranslationService smartTranslationService)
    : IRequestHandler<UpdateTranslationMemoryCommand, Result<Response>>
{
    public async Task<Result<Response>> Handle(UpdateTranslationMemoryCommand command, CancellationToken cancellationToken)
    {
        if (command.Dto is null)
            return Result.Failure<Response>(new Error("TranslationMemory.DataMissing", "Translation memory payload is required", ErrorType.Validation));

        var current = await translationRecordRepository.GetByIdAsync(command.Id, cancellationToken);
        if (current is null)
            return Result.Failure<Response>(new Error("TranslationMemory.NotFound", "Translation memory entry not found", ErrorType.NotFound));

        if (string.IsNullOrWhiteSpace(command.Dto.SourceText) ||
            string.IsNullOrWhiteSpace(command.Dto.TargetLanguage) ||
            string.IsNullOrWhiteSpace(command.Dto.TranslatedText))
        {
            return Result.Failure<Response>(new Error("TranslationMemory.InvalidPayload", "Source text, target language and translated text are required", ErrorType.Validation));
        }

        var oldRequest = new SmartTranslationRequest(current.NormalizedSource, current.TargetLanguage, current.ContextTag);

        var identity = new SmartTranslationRequest(
            command.Dto.SourceText,
            command.Dto.TargetLanguage,
            command.Dto.ContextTag).ToIdentity();

        var duplicate = await translationRecordRepository.GetByIdentityAsync(identity, cancellationToken);
        if (duplicate is not null && duplicate.Id != current.Id)
            return Result.Failure<Response>(new Error("TranslationMemory.Conflict", "Another translation memory entry already exists for this source, language and context", ErrorType.Conflict));

        var updated = await translationRecordRepository.UpdateHumanVerifiedAsync(command.Id, identity, command.Dto.TranslatedText, cancellationToken);
        if (updated is null)
            return Result.Failure<Response>(new Error("TranslationMemory.NotFound", "Translation memory entry not found", ErrorType.NotFound));

        await smartTranslationService.InvalidateCacheAsync(oldRequest, cancellationToken);

        var newRequest = new SmartTranslationRequest(command.Dto.SourceText, command.Dto.TargetLanguage, command.Dto.ContextTag);
        if (newRequest.ToIdentity() != oldRequest.ToIdentity())
            await smartTranslationService.InvalidateCacheAsync(newRequest, cancellationToken);

        return Result.Success(new Response(updated.ToResponse()));
    }
}
