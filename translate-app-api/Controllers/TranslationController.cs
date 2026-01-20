using MediatR;
using Microsoft.AspNetCore.Mvc;
using translate_app.Application.UseCases.Translation.Entities.DTOs;
using translate_app.Application.UseCases.Translation.Translate;

namespace translate_app.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TranslationController: ControllerBase
{
    private readonly IMediator _mediator;

    public TranslationController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> TranslateText([FromBody] TranslationDto request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new TranslateCommand(request), cancellationToken);

        return result.IsSuccess
            ? Ok(result.Value)
            : StatusCode(500, result.Error?.Message);
    }
}