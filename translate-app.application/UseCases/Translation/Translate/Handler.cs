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
    public sealed class Handler: IRequestHandler<Command, Result<Response>>
    {
        private readonly ILanguageRepository _languageRepository;
        private readonly AIService _aiService;

        public Handler(ILanguageRepository languageRepository, AIService aiService)
        {
            _languageRepository = languageRepository;
            _aiService = aiService;
        }


        public async Task<Result<Response>> Handle(Command command, CancellationToken cancellationToken)
        {
            Language? sourceLanguage = null;

            if (command.request.SourceLanguageId != null)
            {
                sourceLanguage = await _languageRepository.GetLanguageById(command.request.SourceLanguageId.Value, cancellationToken);

                if (sourceLanguage == null)
                    return Result.Failure<Response>(new Error("404", "Source language not found"));
            }


            var targetLanguage = await _languageRepository.GetLanguageById(command.request.TargetLanguageId, cancellationToken);
            if (targetLanguage == null)
                return Result.Failure<Response>(new Error("404", "Target language not found"));


            if (string.IsNullOrEmpty(command.request.Text))
                return Result.Failure<Response>(new Error("400", "Request text cannot be null or empty"));


            var translation = await _aiService.TranslateAsync(
                command.request.Text,
                targetLanguage.Name,
                sourceLanguage?.Name,
                cancellationToken
            );
            if (translation == null)
                return Result.Failure<Response>(new Error("400", "Cannot make the translation"));


            var translationRequest = new TranslationResult
            {
                Text = command.request.Text,
                SourceLanguage = sourceLanguage,
                TargetLanguage = targetLanguage,
                Translation = translation,
                UserId = null,
                CreatedAt = DateTime.UtcNow
            };

            return Result.Success(new Response(translationRequest));
        }   
    }
}
