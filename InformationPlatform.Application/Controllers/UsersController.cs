using InformationPlatform.Application.Business.Interfaces;
using InformationPlatform.Application.Models;
using InformationPlatform.Application.Models.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InformationPlatform.Application.Controllers;

[ApiController]
[Route("users")]
[Produces("application/json")]
public class UsersController : ControllerBase
{
    private readonly IUserSettingsBusinessService _settingsService;
    private readonly IChatsBusinessService _chatsService;
    private readonly IPostsBusinessService _postsService;
    private readonly IUsersBusinessService _usersService;

    public UsersController(
        IUserSettingsBusinessService settingsService,
        IChatsBusinessService chatsService,
        IPostsBusinessService postsService,
        IUsersBusinessService usersService)
    {
        _settingsService = settingsService;
        _chatsService = chatsService;
        _postsService = postsService;
        _usersService = usersService;
    }

    [HttpGet]
    public async Task<ActionResult<List<UserDto>>> GetAllAsync(CancellationToken cancellationToken)
    {
        var users = await _usersService.GetAllUsersAsync(cancellationToken);

        return Ok(users);
    }

    [HttpPut("{userId:guid}/settings"),Authorize]
    public async Task<IActionResult> UpdateUserSettingsAsync(Guid userId, [FromBody] UpdateUserSettingsRequest request,
        CancellationToken cancellationToken)
    {
        await _settingsService.UpdateUserSettingsAsync(userId, request, cancellationToken);

        return NoContent();
    }
    
    [HttpGet("{userId:guid}/chats"),Authorize]
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

    [HttpGet("{userId:guid}")]
    public async Task<ActionResult<UserDto?>> GetByIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        var user = await _usersService.GetUserByIdAsync(userId, cancellationToken);
        
        return Ok(user);
    }
}