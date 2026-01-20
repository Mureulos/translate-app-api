/*
    Response é o objeto de saída, retornado após o processamento.
*/

using translate_app.Application.UseCases.Translation.Entities.Response;
using translate_app.Domain.Entities;

namespace translate_app.Application.UseCases.Translation.Translate
{
    public sealed record Response(TranslationResult request)
    {
        public TranslationResponse Translation { get; init; } = new()
        {
            Id = request.Id,
            Text = request.Text,
            Translation = request.Translation,
            SourceLanguage = request.SourceLanguage,
            TargetLanguage = request.TargetLanguage,
            CharacterCount = request.Text.Length,
            CreatedAt = request.CreatedAt
        };
    }
}
