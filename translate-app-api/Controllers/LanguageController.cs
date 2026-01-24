using MediatR;
using Microsoft.AspNetCore.Mvc;
using translate_app.Api.Extensions;
using translate_app.Application.UseCases.Languages.GetAllLanguages;
using translate_app.Application.UseCases.Languages.GetLanguageById;

namespace translate_app.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LanguageController: ControllerBase
{
    private readonly IMediator _mediator;

    public LanguageController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetAllLanguagesQuery(), cancellationToken);
        return this.ToActionResult(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetLanguageByIdQuery(id), cancellationToken);
        return this.ToActionResult(result);
    }
}