using MediatR;
using translate_app.Domain.Abstractions;
using translate_app.Domain.Entities;
using translate_app.Domain.Entities.Response;
using translate_app.Domain.Repositories;

namespace translate_app.application.UseCases.Translation.SaveTranslation;

public sealed class Handler : IRequestHandler<SaveTranslationCommand, Result<Response>>
{
    private readonly ITranslationRepository _translationRepository;

    public Handler(ITranslationRepository translationRepository)
    {
        _translationRepository = translationRepository;
    }

    public async Task<Result<Response>> Handle(SaveTranslationCommand command, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (command?.Dto is null)
            return Result.Failure<Response>(new Error("Translation.MissingData", "Translation data is required", ErrorType.Validation));

        if (string.IsNullOrWhiteSpace(command.Dto.Text) || string.IsNullOrWhiteSpace(command.Dto.TranslationText))
            return Result.Failure<Response>(new Error("Translation.InvalidText", "Text and translation text are required", ErrorType.Validation));

        var translation = new Domain.Entities.Translation
        {
            Text = command.Dto.Text,
            TranslationText = command.Dto.TranslationText,
            SourceLanguageId = command.Dto.SourceLanguageId,
            TargetLanguageId = command.Dto.TargetLanguageId,
            UserId = command.UserId,
            CreatedAt = DateTime.UtcNow
        };

        var saved = await _translationRepository.SaveTranslation(translation, cancellationToken);
        
        return Result.Success(new Response(new SavedTranslationResponse {
            Id = saved.Id,
            Text = saved.Text,
            TranslationText = saved.TranslationText,
            UserId = saved.UserId,
            CreatedAt = saved.CreatedAt    
        }));
    }
}