/*
    Response é o objeto de saída, retornado após o processamento.
*/

using translate_app.Application.UseCases.Translation.Entities.Response;
using translate_app.Domain.Entities;

namespace translate_app.Application.UseCases.Translation.Translate
{
    public sealed record Response(TranslationR request)
    {
        public TranslationResponse Translation { get; init; } = new()
        {
            Id = request.Id,
            Text = request.Text,
            Translation = request.TranslationText,
            SourceLanguage = request.SourceLanguage,
            TargetLanguage = request.TargetLanguage,
            CharacterCount = request.Text.Length,
            CreatedAt = request.CreatedAt
        };
    }
}
