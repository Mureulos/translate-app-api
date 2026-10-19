using MediatR;
using translate_app.Application.UseCases.TranslationMemory.Models;
using translate_app.Domain.Abstractions;
using translate_app.Domain.Repositories;
using translate_app.Domain.Services;

namespace translate_app.Application.UseCases.TranslationMemory.CreateTranslationMemory;

public sealed class Handler : IRequestHandler<CreateTranslationMemoryCommand, Result<Response>>
{
    private readonly ITranslationRecordRepository _translationRecordRepository;
    private readonly ISmartTranslationService _smartTranslationService;

    public Handler(
        ITranslationRecordRepository translationRecordRepository,
        ISmartTranslationService smartTranslationService)
    {
        _translationRecordRepository = translationRecordRepository;
        _smartTranslationService = smartTranslationService;
    }

    public async Task<Result<Response>> Handle(CreateTranslationMemoryCommand command, CancellationToken cancellationToken)
    {
        if (command.Dto is null)
            return Result.Failure<Response>(new Error("TranslationMemory.DataMissing", "Translation memory payload is required", ErrorType.Validation));

        if (string.IsNullOrWhiteSpace(command.Dto.SourceText) ||
            string.IsNullOrWhiteSpace(command.Dto.TargetLanguage) ||
            string.IsNullOrWhiteSpace(command.Dto.TranslatedText))
        {
            return Result.Failure<Response>(new Error("TranslationMemory.InvalidPayload", "Source text, target language and translated text are required", ErrorType.Validation));
        }

        var identity = new SmartTranslationRequest(
            command.Dto.SourceText,
            command.Dto.TargetLanguage,
            command.Dto.ContextTag).ToIdentity();

        var existing = await _translationRecordRepository.GetByIdentityAsync(identity, cancellationToken);
        if (existing is not null)
        {
            if (existing.IsHumanVerified)
                return Result.Failure<Response>(new Error("TranslationMemory.Conflict", "A translation memory entry already exists for this source, language and context", ErrorType.Conflict));

            var promoted = await _translationRecordRepository.UpdateHumanVerifiedAsync(
                existing.Id,
                identity,
                command.Dto.TranslatedText,
                cancellationToken);

            if (promoted is null)
                return Result.Failure<Response>(new Error("TranslationMemory.NotFound", "Translation memory entry not found", ErrorType.NotFound));

            await _smartTranslationService.InvalidateCacheAsync(
                new SmartTranslationRequest(
                    command.Dto.SourceText,
                    command.Dto.TargetLanguage,
                    command.Dto.ContextTag),
                cancellationToken);

            return Result.Success(new Response(promoted.ToResponse()));
        }

        var created = await _translationRecordRepository.CreateHumanVerifiedAsync(identity, command.Dto.TranslatedText, cancellationToken);
        await _smartTranslationService.InvalidateCacheAsync(
            new SmartTranslationRequest(
                command.Dto.SourceText,
                command.Dto.TargetLanguage,
                command.Dto.ContextTag),
            cancellationToken);

        return Result.Success(new Response(created.ToResponse()));
    }
}
