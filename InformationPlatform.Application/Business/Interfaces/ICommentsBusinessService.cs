using InformationPlatform.Application.Models;
using InformationPlatform.Application.Models.Requests;

namespace InformationPlatform.Application.Business.Interfaces;

public interface ICommentsBusinessService
{
    Task<List<CommentDto>> GetCommentsByPostIdAsync(Guid postId, CancellationToken cancellationToken);
    Task AddCommentAsync(AddCommentRequest request, CancellationToken cancellationToken);
}