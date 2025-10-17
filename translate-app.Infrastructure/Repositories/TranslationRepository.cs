/** 
 * O Repository,é responsável por expor métodos básicos de acesso a dados 
 * (ex: buscar, salvar, atualizar) sem conter lógica de negócio ou 
 * regras da aplicação, apenas persistência e recuperação dos dados.
**/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using translate_app.Domain.Repositories;
using translate_app.Infrastructure.Data;

namespace translate_app.Infrastructure.Repositories
{
    public class TranslationRepository(AppDbContext context) : ITranslationRepository
    {
        public Task<string> GetTranslationAsync(string text, string targetLanguage, string? sourceLanguage)
        {
            throw new NotImplementedException();
        }
    }
}