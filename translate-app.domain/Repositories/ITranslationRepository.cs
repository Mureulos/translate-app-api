using translate_app.Domain.Entities;

namespace translate_app.Domain.Repositories
{
    public interface ITranslationRepository : IRepository<Translation>
    {
        Task<Translation> SaveTranslation(Translation dto, CancellationToken cancellationToken = default);
    }
}