using InformationPlatform.Application.Business.Interfaces;
using InformationPlatform.Application.Helpers.Interfaces;
using InformationPlatform.Application.Models;
using InformationPlatform.Application.Models.Requests;
using InformationPlatform.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InformationPlatform.Application.Controllers;

[Authorize]
[ApiController]
[Route("chats")]
[Produces("application/json")]
public class ChatsController : ControllerBase
{
    private readonly IChatsBusinessService _chatsService;
    private readonly IMessagesBusinessService _messagesService;

    public ChatsController(
        IChatsBusinessService chatsService,
        IMessagesBusinessService messagesService)
    {
        _chatsService = chatsService;
        _messagesService = messagesService;
    }
    
    [HttpGet("{chatId:guid}/messages")]
    public async Task<ActionResult<List<MessageDto>>> GetMessagesAsync(Guid chatId, CancellationToken cancellationToken)
    {
        var messages = await _messagesService.GetMessagesByChatIdAsync(chatId, cancellationToken);

        return Ok(messages);
    }

    [HttpPost]
    public async Task<IActionResult> AddAsync([FromBody] CreateChatRequest request, CancellationToken cancellationToken)
    {
        await _chatsService.AddChatAsync(request, cancellationToken);

        return Created();
    }

    [HttpPut("{chatId:guid}")]
    public async Task<IActionResult> UpdateAsync(Guid chatId, [FromBody] UpdateChatRequest request,
        CancellationToken cancellationToken)
    {
        await _chatsService.UpdateChatAsync(chatId, request, cancellationToken);
        
        return NoContent();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteAsync([FromQuery] List<Guid> ids, CancellationToken cancellationToken)
    {
        await _chatsService.DeleteChatsAsync(ids, cancellationToken);

        return NoContent();
    }

    [HttpPatch("{chatId:guid}")]
    public async Task<IActionResult> AddParticipantsAsync(Guid chatId, [FromQuery] List<Guid> participantIds,
        CancellationToken cancellationToken)
    {
        await _chatsService.AddParticipantsAsync(chatId, participantIds, cancellationToken);
        
        return NoContent();
    }
}