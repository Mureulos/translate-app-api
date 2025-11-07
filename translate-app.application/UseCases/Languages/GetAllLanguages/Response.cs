using translate_app.Domain.Entities;

namespace translate_app.Application.UseCases.Languages.GetAllLanguages
{
    public sealed record Response(LanguageResponse[] Languages);
}
