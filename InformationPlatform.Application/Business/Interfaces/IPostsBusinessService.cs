using InformationPlatform.Application.Models;
using InformationPlatform.Application.Models.Requests;
using Templates.Business;

namespace InformationPlatform.Application.Business.Interfaces;

public interface IPostsBusinessService : IBaseBusinessService
{
    Task<List<PostDto>> GetPostsByUserIdAsync(Guid userId, CancellationToken cancellationToken);
    Task<List<PostDto>> GetAllPostsAsync(CancellationToken cancellationToken);
    Task<PostDto> AddPostAsync(CreatePostRequest request, CancellationToken cancellationToken);
    Task DeletePostAsync(Guid postId, CancellationToken cancellationToken);
}