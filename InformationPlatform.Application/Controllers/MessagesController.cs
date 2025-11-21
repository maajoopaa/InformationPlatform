using InformationPlatform.Application.Business.Interfaces;
using InformationPlatform.Application.Models;
using InformationPlatform.Application.Models.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InformationPlatform.Application.Controllers;

[Authorize]
[ApiController]
[Route("messages")]
[Produces("application/json")]
public class MessagesController : ControllerBase
{
    private readonly IMessagesBusinessService _service;

    public MessagesController(IMessagesBusinessService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> AddAsync([FromBody] SendMessageRequest request, CancellationToken cancellationToken)
    {
        await _service.AddMessageAsync(request, cancellationToken);

        return Created();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteAsync([FromQuery] List<Guid> messageIds, CancellationToken cancellationToken)
    {
        await _service.DeleteMessagesAsync(messageIds, cancellationToken);

        return NoContent();
    }
}