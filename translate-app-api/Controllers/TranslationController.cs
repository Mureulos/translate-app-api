using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using translate_app.Api.Extensions;
using translate_app.Application.UseCases.Translation.Entities.DTOs;
using translate_app.application.UseCases.Translation.GetSavedTranslations;
using translate_app.application.UseCases.Translation.SaveTranslation;
using translate_app.Application.UseCases.Translation.Translate;
using translate_app.application.UseCases.Translation.TranslateFile;
using translate_app.Domain.Entities.DTOs;

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
    
    [Authorize]
    [HttpPost("from-file")]
    public async Task<IActionResult> TranslateFromFile( 
        IFormFile file, 
        [FromForm] int targetLanguageId, 
        [FromForm] int? sourceLanguageId, 
        CancellationToken cancellationToken)
    {
        var extractResult = await _mediator.Send(new ExtractTextFromFileCommand(file), cancellationToken);

        if (!extractResult.IsSuccess)
            return this.ToActionResult(extractResult);

        var textDto = new TranslationDto
        {
            Text = extractResult.Value.response.ExtractedContent,
            SourceLanguageId = sourceLanguageId,
            TargetLanguageId = targetLanguageId
        };

        var translateResult = await _mediator.Send(new TranslateTextCommand(textDto), cancellationToken);

        return this.ToActionResult(translateResult);
    }

    [Authorize] 
    [HttpPost("save")]
    public async Task<IActionResult> SaveTranslation([FromBody] SaveTranslationDto request, CancellationToken cancellationToken)
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var userId))
            return Unauthorized();

        var translateResult = await _mediator.Send(new SaveTranslationCommand(request, userId), cancellationToken); 
        return this.ToActionResult(translateResult);
    }
    
    [Authorize] 
    [HttpGet("save")]
    public async Task<IActionResult> GetSavedTranslations(CancellationToken cancellationToken)
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var userId))
            return Unauthorized();

        var translateResult = await _mediator.Send(new GetSavedTranslationsQuery(userId), cancellationToken); 
        return this.ToActionResult(translateResult);
    }
}