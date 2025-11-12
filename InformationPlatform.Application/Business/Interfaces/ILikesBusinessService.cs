using InformationPlatform.Application.Models;
using InformationPlatform.Application.Models.Requests;

namespace InformationPlatform.Application.Business.Interfaces;

public interface ILikesBusinessService
{
    Task<List<LikeDto>> GetLikesByPostIdAsync(Guid postId, CancellationToken cancellationToken);
    Task AddLikeAsync(AddLikeRequest request, CancellationToken cancellationToken);
    Task DeleteLikeAsync(Guid likeId, CancellationToken cancellationToken);
}