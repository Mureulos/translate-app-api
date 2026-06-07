using MediatR;
using Microsoft.AspNetCore.Mvc;
using translate_app.Api.Extensions;
using translate_app.Application.UseCases.User.DeleteUser;
using translate_app.Application.UseCases.User.GetAllUsers;
using translate_app.Application.UseCases.User.GetUserByEmail;
using translate_app.Application.UseCases.User.GetUserById;
using translate_app.Application.UseCases.User.UpdateUser;
using translate_app.Domain.Entities.DTOs;

namespace translate_app.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetAllUsersQuery(), cancellationToken);
        return this.ToActionResult(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetUserByIdQuery(id), cancellationToken);
        return this.ToActionResult(result);
    }

    [HttpGet("{email}")]
    public async Task<IActionResult> GetByEmail(string email, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetUserByEmailQuery(email), cancellationToken);
        return this.ToActionResult(result);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UserDto dto, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new UpdateUserCommand(dto.Id, dto), cancellationToken);
        return this.ToActionResult(result);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new DeleteUserCommand(id), cancellationToken);
        return this.ToActionResult(result);
    }
}