using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using translate_app.Application.UseCases.Translation.Translate;
using translate_app.Domain.Repositories;
using translate_app.Infrastructure.Repositories;

namespace translate_app.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class TranslationController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ILanguageRepository _languageRepository;

        public TranslationController(IMediator mediator, ILanguageRepository languageRepository)
        {
            _mediator = mediator;
            _languageRepository = languageRepository;
        }


        [HttpPost("")]
        public async Task<IActionResult> TranslateText([FromBody] TranslationRequestDto request, CancellationToken cancellationToken)
        {
            if (request is null) 
                return BadRequest();

            try
            {
                var result = await _mediator.Send(new Command(request), cancellationToken);

                if (!result.IsSuccess) 
                    return StatusCode(500, result.Error?.Message);
                
                return Ok(result.Value);
            }
            catch (OperationCanceledException)
            {
                return StatusCode(499);
            }
        }
    }
}
