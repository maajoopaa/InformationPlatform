using InformationPlatform.Application.Models;
using InformationPlatform.Application.Models.Requests;
using Templates.Business;

namespace InformationPlatform.Application.Business.Interfaces;

public interface ILikesBusinessService : IBaseBusinessService
{
    Task<List<LikeDto>> GetLikesByPostIdAsync(Guid postId, CancellationToken cancellationToken);
    Task<LikeDto> AddLikeAsync(AddLikeRequest request, CancellationToken cancellationToken);
    Task DeleteLikeAsync(Guid likeId, CancellationToken cancellationToken);
}