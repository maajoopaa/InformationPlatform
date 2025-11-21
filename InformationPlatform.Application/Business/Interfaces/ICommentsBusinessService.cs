using InformationPlatform.Application.Models;
using InformationPlatform.Application.Models.Requests;
using Templates.Business;

namespace InformationPlatform.Application.Business.Interfaces;

public interface ICommentsBusinessService : IBaseBusinessService
{
    Task<List<CommentDto>> GetCommentsByPostIdAsync(Guid postId, CancellationToken cancellationToken);
    Task<CommentDto> AddCommentAsync(AddCommentRequest request, CancellationToken cancellationToken);
}