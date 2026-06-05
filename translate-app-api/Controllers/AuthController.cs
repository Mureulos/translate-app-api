using MediatR;
using Microsoft.AspNetCore.Mvc;
using translate_app.Api.Extensions;
using translate_app.Application.UseCases.Auth.Login;
using translate_app.Application.UseCases.Auth.Logout;
using translate_app.Application.UseCases.Auth.Signin;
using translate_app.Domain.Entities.DTOs;

namespace translate_app.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost("signin")]
    public async Task<IActionResult> Signin([FromBody] UserDto dto, CancellationToken cancellationToken)
    {
        var command = new SiginCommand(dto);
        
        var result = await _mediator.Send(command, cancellationToken);
        return this.ToActionResult(result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] AuthDto dto, CancellationToken cancellationToken)
    {
        var command = new LoginCommand(dto);

        var result = await _mediator.Send(command, cancellationToken);
        return this.ToActionResult(result);
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new LogoutCommand(), cancellationToken);
        return this.ToActionResult(result);
    }
}
