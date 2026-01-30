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
    public sealed class Handler: IRequestHandler<TranslateCommand, Result<Response>>
    {
        private readonly ILanguageRepository _languageRepository;
        private readonly AIService _aiService;
        private int _charactersLimit = 300;

        public Handler(ILanguageRepository languageRepository, AIService aiService)
        {
            _languageRepository = languageRepository;
            _aiService = aiService;
        }

        public async Task<Result<Response>> Handle(TranslateCommand command, CancellationToken cancellationToken)
        {
            Language? sourceLanguage = null;

            if (command.Dto.SourceLanguageId != null)
            {
                sourceLanguage = await _languageRepository.GetLanguageById(command.Dto.SourceLanguageId.Value, cancellationToken);

                if (sourceLanguage == null)
                    return Result.Failure<Response>(new Error("404", "Source language not found"));
            }


            var targetLanguage = await _languageRepository.GetLanguageById(command.Dto.TargetLanguageId, cancellationToken);

            if (targetLanguage == null)
                return Result.Failure<Response>(new Error("404", "Target language not found"));

            if (string.IsNullOrEmpty(command.Dto.Text))
                return Result.Failure<Response>(new Error("400", "dto text cannot be null or empty"));

            if (command.Dto.Text.Length > _charactersLimit)
                return Result.Failure<Response>(new Error("400", $"Your dto text cannot have more than {300} characters"));


            var translation = await _aiService.TranslateAsync(
                command.Dto.Text,
                targetLanguage.Name,
                sourceLanguage?.Name,
                cancellationToken
            );

            if (translation == null)
                return Result.Failure<Response>(new Error("400", "Cannot make the translation"));


            var translationDto = new TranslationR
            {
                Text = command.Dto.Text,
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
