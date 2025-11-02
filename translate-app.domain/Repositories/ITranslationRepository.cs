using translate_app.Domain.Entities;

namespace translate_app.Domain.Repositories
{
    public interface ITranslationRepository : IRepository<TranslationRequest>
    {
        Task<TranslationRequest> TranslateText(string text, string TargetLanguage, string? SourceLanguage, CancellationToken cancellationToken = default);
    }
}