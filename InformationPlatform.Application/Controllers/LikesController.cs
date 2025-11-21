using InformationPlatform.Application.Business.Interfaces;
using InformationPlatform.Application.Models.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InformationPlatform.Application.Controllers;

[Authorize]
[ApiController]
[Route("likes")]
[Produces("application/json")]
public class LikesController : ControllerBase
{
    private readonly ILikesBusinessService _service;

    public LikesController(ILikesBusinessService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> AddAsync([FromBody] AddLikeRequest request, CancellationToken cancellationToken)
    {
        await _service.AddLikeAsync(request,cancellationToken);

        return Created();
    }

    [HttpDelete("{likeId:guid}")]
    public async Task<IActionResult> DeleteAsync(Guid likeId, CancellationToken cancellationToken)
    {
        await _service.DeleteLikeAsync(likeId, cancellationToken);
        
        return NoContent();
    }
}