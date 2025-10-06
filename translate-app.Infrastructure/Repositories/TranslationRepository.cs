using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using translate_app.Domain.Repositories;
using translate_app.Infrastructure.Data;

namespace translate_app.Infrastructure.Repositories
{
    public class TranslationRepository(AppDbContext context): ITranslationRepository
    {
    }
}
