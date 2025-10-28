using translate_app.domain.Models;

namespace translate_app.Domain.Repositories
{
    public interface ITranslationRepository : IRepository<TranslationRequest>
    {
        Task<string> GetTranslationAsync(string text, string TargetLanguage, string? SourceLanguage);
    }
}