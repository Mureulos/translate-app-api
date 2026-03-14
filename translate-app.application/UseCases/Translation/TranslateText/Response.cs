/*
    Response é o objeto de saída, retornado após o processamento.
*/

namespace translate_app.Application.UseCases.Translation.Translate;
public sealed record Response(Domain.Entities.Translation response);