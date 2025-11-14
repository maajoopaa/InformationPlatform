using InformationPlatform.Application.Business.Interfaces;
using InformationPlatform.Application.Models;
using InformationPlatform.Application.Models.Requests;
using Microsoft.AspNetCore.Mvc;

namespace InformationPlatform.Application.Controllers;

[ApiController]
[Route("users")]
public class UsersController : ControllerBase
{
    private readonly IUserSettingsBusinessService _settingsService;
    private readonly IChatsBusinessService _chatsService;
    private readonly IPostsBusinessService _postsService;

    public UsersController(
        IUserSettingsBusinessService settingsService,
        IChatsBusinessService chatsService,
        IPostsBusinessService postsService)
    {
        _settingsService = settingsService;
        _chatsService = chatsService;
        _postsService = postsService;
    }

    [HttpPut("{userId:guid}/settings")]
    public async Task<IActionResult> UpdateUserSettingsAsync(Guid userId, [FromBody] UpdateUserSettingsRequest request,
        CancellationToken cancellationToken)
    {
        await _settingsService.UpdateUserSettingsAsync(userId, request, cancellationToken);

        return NoContent();
    }
    
    [HttpGet("{userId:guid}/chats")]
    public async Task<ActionResult<List<ChatDto>>> GetChatsAsync(Guid userId, CancellationToken cancellationToken)
    {
        var chats = await _chatsService.GetChatsByUserIdAsync(userId, cancellationToken);
        
        return Ok(chats);
    }
    
    [HttpGet("{userId:guid}/posts")]
    public async Task<ActionResult<List<PostDto>>> GetPostsAsync(Guid userId, CancellationToken cancellationToken)
    {
        var posts = await _postsService.GetPostsByUserIdAsync(userId, cancellationToken);

        return Ok(posts);
    }
}