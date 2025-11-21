using InformationPlatform.Application.Business.Interfaces;
using InformationPlatform.Application.Models;
using InformationPlatform.Application.Models.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InformationPlatform.Application.Controllers;

[ApiController]
[Route("posts")]
[Produces("application/json")]
public class PostsController : ControllerBase
{
    private readonly IPostsBusinessService _postsService;
    private readonly ICommentsBusinessService _commentsService;
    private readonly ILikesBusinessService _likesService;

    public PostsController(
        IPostsBusinessService postsService,
        ICommentsBusinessService commentsService,
        ILikesBusinessService  likesService)
    {
        _postsService = postsService;
        _commentsService = commentsService;
        _likesService = likesService;
    }
    
    [HttpGet("{postId:guid}/comments")]
    public async Task<ActionResult<List<CommentDto>>> GetCommentsAsync(Guid postId,
        CancellationToken cancellationToken)
    {
        var comments = await _commentsService.GetCommentsByPostIdAsync(postId, cancellationToken);

        return Ok(comments);
    }
    
    [HttpGet("{postId:guid}/likes")]
    public async Task<ActionResult<List<LikeDto>>> GetLikesAsync(Guid postId, CancellationToken cancellationToken)
    {
        var likes = await _likesService.GetLikesByPostIdAsync(postId, cancellationToken);

        return Ok(likes);
    }

    [HttpGet]
    public async Task<ActionResult<List<PostDto>>> GetAllAsync(CancellationToken cancellationToken)
    {
        var posts = await _postsService.GetAllPostsAsync(cancellationToken);

        return Ok(posts);
    }

    [HttpPost,Authorize]
    public async Task<ActionResult<PostDto>> AddAsync([FromBody] CreatePostRequest request, CancellationToken cancellationToken)
    {
        var post = await _postsService.AddPostAsync(request, cancellationToken);

        return Ok(post);
    }

    [HttpDelete("{postId:guid}"),Authorize]
    public async Task<IActionResult> DeleteAsync(Guid postId, CancellationToken cancellationToken)
    {
        await _postsService.DeletePostAsync(postId, cancellationToken);

        return NoContent();
    }
}