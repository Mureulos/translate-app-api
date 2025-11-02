/*
    Handler é o componente que processa o Command
*/

using MediatR;
using translate_app.Domain.Entities;
using translate_app.Domain.Abstractions;
using translate_app.Domain.Repositories;

namespace translate_app.Application.UseCases.Translation.Translate
{
    public sealed class Handler: IRequestHandler<Command, Result<Response>>
    {
        private readonly ILanguageRepository _languageRepository;

        public Handler(ILanguageRepository languageRepository)
        {
            _languageRepository = languageRepository;
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


            var translationRequest = new TranslationRequest
            {
                Text = command.request.Text,
                SourceLanguage = sourceLanguage,
                TargetLanguage = targetLanguage,
                Translation = "Hello world!",
                UserId = null,
                CreatedAt = DateTime.UtcNow
            };

            return Result.Success(new Response(translationRequest));
        }   
    }
}
