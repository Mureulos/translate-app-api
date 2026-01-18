using Microsoft.AspNetCore.Mvc;
using translate_app.Domain.Repositories;
using translate_app.Infrastructure.Repositories;

namespace translate_app.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class LanguageController : ControllerBase
    {
        private readonly ILanguageRepository _languageRepository;

        public LanguageController(ILanguageRepository languageRepository) 
        {
            _languageRepository = languageRepository;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAllLanguages(CancellationToken cancellationToken)
        {
            var languages = await _languageRepository.GetAllLanguages(cancellationToken);
            return Ok(languages);
        }

        [HttpGet("{idLanguage}")]
        public async Task<IActionResult> GetLanguageById(int idLanguage, CancellationToken cancellationToken)
        {
            var languages = await _languageRepository.GetLanguageById(idLanguage, cancellationToken);
            return Ok(languages);
        }
    }
}
