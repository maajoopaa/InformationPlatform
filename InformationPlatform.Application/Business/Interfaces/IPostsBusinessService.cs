using InformationPlatform.Application.Models;
using InformationPlatform.Application.Models.Requests;

namespace InformationPlatform.Application.Business.Interfaces;

public interface IPostsBusinessService
{
    Task<List<PostDto>> GetPostsByUserIdAsync(Guid userId, CancellationToken cancellationToken);
    Task<List<PostDto>> GetAllPostsAsync(CancellationToken cancellationToken);
    Task AddPostAsync(CreatePostRequest request, CancellationToken cancellationToken);
    Task DeletePostAsync(Guid postId, CancellationToken cancellationToken);
}