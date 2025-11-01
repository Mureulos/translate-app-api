/*
    Handler é o componente que processa o Command
*/

using MediatR;
using translate_app.domain.Models;
using translate_app.Domain.Abstractions;
using translate_app.Domain.Repositories;

namespace translate_app.Application.UseCases.Translation.Translate
{
    public sealed class Handler(ITranslationRepository repository) : IRequestHandler<Command, Result<Response>>
    {
        public async Task<Result<Response>> Handle(Command command, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(command.Request.Text))
                return Result.Failure<Response>(new Error("400", "Request text cannot be null or empty"));

            cancellationToken.ThrowIfCancellationRequested();


            var translationRequest = new TranslationRequest
            {
                Text = command.Request.Text,
                SourceLanguage = command.Request.SourceLanguage,
                TargetLanguage = command.Request.TargetLanguage,
                Translation = "Hello world!",
                UserId = null,
                CreatedAt = DateTime.UtcNow
            };

            return Result.Success(new Response(translationRequest));
        }   
    }
}
