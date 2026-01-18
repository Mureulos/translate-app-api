using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using translate_app.Application.UseCases.Languages.Entities.Response;
using translate_app.Domain.Entities;

namespace translate_app.Application.UseCases.Languages.GetLanguageById
{
    public sealed record Response(LanguageResponse language);
}
