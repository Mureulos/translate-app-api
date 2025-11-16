/*
    Response é o objeto de saída, retornado após o processamento.
*/

using translate_app.Domain.Entities;

namespace translate_app.Application.UseCases.Translation.Translate
{
    public sealed class Response
    {
        public TranslationResultDto Translation { get; }

        public Response(TranslationResult translationRequest)
        {
            Translation = new TranslationResultDto
            {
                Id = translationRequest.Id,
                Text = translationRequest.Text,
                Translation = translationRequest.Translation,
                SourceLanguage = translationRequest.SourceLanguage,
                TargetLanguage = translationRequest.TargetLanguage,
                CharacterCount = translationRequest.Text.Length,
                CreatedAt = translationRequest.CreatedAt
            };
        }
    }
}
