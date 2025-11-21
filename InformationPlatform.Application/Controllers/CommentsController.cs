using InformationPlatform.Application.Business.Interfaces;
using InformationPlatform.Application.Models;
using InformationPlatform.Application.Models.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InformationPlatform.Application.Controllers;

[ApiController]
[Route("comments")]
[Produces("application/json")]
public class CommentsController : ControllerBase
{
    private readonly ICommentsBusinessService _service;

    public CommentsController(ICommentsBusinessService service)
    {
        _service = service;
    }

    [HttpPost,Authorize]
    public async Task<ActionResult<CommentDto>> AddAsync([FromBody] AddCommentRequest request, CancellationToken cancellationToken)
    {
        var comment = await _service.AddCommentAsync(request, cancellationToken);

        return Ok(comment);
    }
}