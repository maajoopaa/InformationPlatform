using InformationPlatform.Application.Business.Interfaces;
using InformationPlatform.Application.Models;
using InformationPlatform.Application.Models.Requests;
using Microsoft.AspNetCore.Mvc;

using LoginRequest = InformationPlatform.Application.Models.Requests.LoginRequest;

namespace InformationPlatform.Application.Controllers;

[ApiController]
[Route("authorization")]
public class AuthorizationController : ControllerBase
{
    private readonly IAuthorizationBusinessService _service;

    public AuthorizationController(IAuthorizationBusinessService service)
    {
        _service = service;
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthorizationResponse>> LoginAsync([FromBody] LoginRequest request,
        CancellationToken cancellationToken)
    {
        var response = await _service.LoginAsync(request, cancellationToken);

        return Ok(response);
    }
    
    [HttpPost("register")]
    public async Task<ActionResult<AuthorizationResponse>> RegisterAsync([FromBody] CreateUserRequest request,
        CancellationToken cancellationToken)
    {
        var response = await _service.RegisterAsync(request, cancellationToken);

        return Ok(response);
    }
}