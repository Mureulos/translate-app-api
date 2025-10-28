using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using translate_app.domain.Models;

namespace translate_app.Application.UseCases.Translation.Translate
{
    public sealed record Response(TranslationRequest translation);
}
