using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using translate_app.domain.Models;
using translate_app.Domain.Repositories;

namespace translate_app.Domain.Repositories
{
    public interface ITranslationRepository : IRepository<TranslationRequest>
    {
        Task<string> GetTranslationAsync(string text, string targetLanguage, string? sourceLanguage);
    }
}