using MediatR;
using Microsoft.AspNetCore.Mvc;
using translate_app.Api.Extensions;
using translate_app.Application.UseCases.Translation.Entities.DTOs;
using translate_app.Application.UseCases.Translation.Translate;
using translate_app.application.UseCases.Translation.TranslateFile;

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
        var result = await _mediator.Send(new TranslateTextCommand(request), cancellationToken);
        return this.ToActionResult(result);
    }
    
    [HttpPost("upload")]
    public async Task<IActionResult> TranslateFileCommand([FromForm] IFormFile file, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new TranslateFileCommand(file), cancellationToken);
        return this.ToActionResult(result);
    }
}