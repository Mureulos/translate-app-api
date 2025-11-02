/*
    Response é o objeto de saída, retornado após o processamento.
*/

using translate_app.Domain.Entities;

namespace translate_app.Application.UseCases.Translation.Translate
{
    public sealed record Response(TranslationRequest translation);
}
