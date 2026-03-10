/*
    Handler é o componente que processa o Command
*/

using MediatR;
using translate_app.Domain.Entities;
using translate_app.Domain.Abstractions;
using translate_app.Domain.Repositories;
using translate_app.Infrastructure.Services;

namespace translate_app.Application.UseCases.Translation.Translate
{
    public sealed class Handler: IRequestHandler<TranslateTextCommand, Result<Response>>
    {
        private readonly ILanguageRepository _languageRepository;
        private readonly AIService _aiService;
        private int _charactersLimit = 300;

        public Handler(ILanguageRepository languageRepository, AIService aiService)
        {
            _languageRepository = languageRepository;
            _aiService = aiService;
        }

        public async Task<Result<Response>> Handle(TranslateTextCommand textCommand, CancellationToken cancellationToken)
        {
            Language? sourceLanguage = null;

            if (textCommand.Dto.SourceLanguageId != null)
            {
                sourceLanguage = await _languageRepository.GetLanguageById(textCommand.Dto.SourceLanguageId.Value, cancellationToken);

                if (sourceLanguage == null)
                    return Result.Failure<Response>(new Error("404", "Source language not found"));
            }


            var targetLanguage = await _languageRepository.GetLanguageById(textCommand.Dto.TargetLanguageId, cancellationToken);

            if (targetLanguage == null)
                return Result.Failure<Response>(new Error("404", "Target language not found"));

            if (string.IsNullOrEmpty(textCommand.Dto.Text))
                return Result.Failure<Response>(new Error("400", "dto text cannot be null or empty"));

            if (textCommand.Dto.Text.Length > _charactersLimit)
                return Result.Failure<Response>(new Error("400", $"Your dto text cannot have more than {300} characters"));


            var translation = await _aiService.TranslateAsync(
                textCommand.Dto.Text,
                targetLanguage.Name,
                sourceLanguage?.Name,
                cancellationToken
            );

            if (translation == null)
                return Result.Failure<Response>(new Error("400", "Cannot make the translation"));


            var translationDto = new TranslationR
            {
                Text = textCommand.Dto.Text,
                SourceLanguage = sourceLanguage,
                TargetLanguage = targetLanguage,
                TranslationText = translation,
                UserId = null,
                CreatedAt = DateTime.UtcNow
            };

            return Result.Success(new Response(translationDto));
        }   
    }
}
