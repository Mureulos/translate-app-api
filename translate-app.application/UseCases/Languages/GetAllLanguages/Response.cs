using translate_app.Application.UseCases.Languages.Entities.Response;
using translate_app.Domain.Entities;

namespace translate_app.Application.UseCases.Languages.GetAllLanguages
{
    public sealed record Response(LanguageResponse[] Languages);
}
