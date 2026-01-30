using translate_app.Application.UseCases.Languages.Entities.Response;

namespace translate_app.Application.UseCases.Languages.GetAllLanguages
{
    public sealed record Response(LanguageResponse[] response);
}
