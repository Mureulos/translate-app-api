using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using translate_app.Domain.Entities;

namespace translate_app.Domain.Repositories
{
    public interface ILanguageRepository: IRepository<Language>
    {
        Task<IEnumerable<Language>> GetAllLanguages(CancellationToken cancellationToken);
        Task<Language> GetLanguageById(int idLanguage, CancellationToken cancellationToken);
    }
}
