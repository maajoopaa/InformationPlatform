using AutoMapper;
using InformationPlatform.Application.Business.Interfaces;
using InformationPlatform.Application.Exceptions;
using InformationPlatform.Application.Models;
using InformationPlatform.Application.Models.Requests;
using InformationPlatform.Domain.Models;
using InformationPlatform.Repository;

namespace InformationPlatform.Application.Business;

public class LikesBusinessService : ILikesBusinessService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public LikesBusinessService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    public async Task<List<LikeDto>> GetLikesByPostIdAsync(Guid postId, CancellationToken cancellationToken)
    {
        var likeEntities = await _unitOfWork.Likes
            .GetAsync(x =>  x.PostId == postId, cancellationToken);

        return _mapper.Map<List<LikeDto>>(likeEntities);
    }

    public async Task AddLikeAsync(AddLikeRequest request, CancellationToken cancellationToken)
    {
        var postEntity = await _unitOfWork.Posts.GetByIdAsync(request.PostId, cancellationToken);

        if (postEntity == null)
        {
            throw new NotFoundException("Такого поста не существует.");
        }
        
        var likeEntity = _mapper.Map<DbLike>(request);
        likeEntity.CreatedAt = DateTime.UtcNow;
        
        await _unitOfWork.Likes.AddAsync(likeEntity, cancellationToken);
    }

    public async Task DeleteLikeAsync(Guid likeId, CancellationToken cancellationToken)
    {
        var likeEntity = await _unitOfWork.Likes.GetByIdAsync(likeId, cancellationToken);

        if (likeEntity == null)
        {
            throw new NotFoundException("Такого лайка не существует.");
        }
        
        await _unitOfWork.Likes.DeleteAsync(likeEntity, cancellationToken);
    }
}