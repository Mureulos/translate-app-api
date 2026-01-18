/** 
 * O Repository,é responsável por expor métodos básicos de acesso a dados 
 * (ex: buscar, salvar, atualizar) sem conter lógica de negócio ou 
 * regras da aplicação, apenas persistência e recuperação dos dados.
**/

using translate_app.Domain.Entities;
using translate_app.Domain.Repositories;
using translate_app.Infrastructure.Data;

namespace translate_app.Infrastructure.Repositories
{
    public class TranslationRepository(AppDbContext context) : ITranslationRepository
    {
        Task<TranslationResult> ITranslationRepository
        .TranslateText(
            string text, 
            string TargetLanguage, 
            string? SourceLanguage, 
            CancellationToken cancellationToken
        )
        {
            throw new NotImplementedException();
        }
    }
}