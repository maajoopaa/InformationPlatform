using AutoMapper;
using InformationPlatform.Application.Business.Interfaces;
using InformationPlatform.Application.Exceptions;
using InformationPlatform.Application.Models;
using InformationPlatform.Application.Models.Requests;
using InformationPlatform.Domain.Models;
using InformationPlatform.Repository;
using Templates.Business;

namespace InformationPlatform.Application.Business;

public class CommentsBusinessService :BaseBusinessService, ICommentsBusinessService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CommentsBusinessService(
        IUnitOfWork unitOfWork, 
        IMapper mapper,
        IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    public async Task<List<CommentDto>> GetCommentsByPostIdAsync(Guid postId, CancellationToken cancellationToken)
    {
        var commentEntities = await _unitOfWork.Comments
            .GetAsync(x => x.PostId == postId,cancellationToken);
        
        return _mapper.Map<List<CommentDto>>(commentEntities);
    }

    public async Task AddCommentAsync(AddCommentRequest request, CancellationToken cancellationToken)
    {
        var postEntity = await _unitOfWork.Posts.GetByIdAsync(request.PostId, cancellationToken);

        if (postEntity == null)
        {
            throw new NotFoundException("Такого поста не существует.");
        }

        var commentEntity = _mapper.Map<DbComment>(request);
        commentEntity.CreatedAt = DateTime.UtcNow;

        await _unitOfWork.Comments.AddAsync(commentEntity,cancellationToken);
    }
}