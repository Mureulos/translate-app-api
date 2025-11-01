using translate_app.domain.Models;

namespace translate_app.Domain.Repositories
{
    public interface ITranslationRepository : IRepository<TranslationRequest>
    {
        Task<TranslationRequest> TranslateText(string text, string TargetLanguage, string? SourceLanguage, CancellationToken cancellationToken = default);
    }
}