using AutoMapper;
using InformationPlatform.Application.Business.Interfaces;
using InformationPlatform.Application.Exceptions;
using InformationPlatform.Application.Helpers.Interfaces;
using InformationPlatform.Application.Models;
using InformationPlatform.Application.Models.Requests;
using InformationPlatform.Domain.Models;
using InformationPlatform.Repository;
using Templates.Business;

namespace InformationPlatform.Application.Business;

public class LikesBusinessService :BaseBusinessService, ILikesBusinessService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IPermissionsService _permissionsService;

    public LikesBusinessService(
        IUnitOfWork unitOfWork, 
        IMapper mapper,
        IHttpContextAccessor httpContextAccessor,
        IPermissionsService permissionsService) : base(httpContextAccessor)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _permissionsService = permissionsService;
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
        likeEntity.CreatedById = UserId;
        
        await _unitOfWork.Likes.AddAsync(likeEntity, cancellationToken);
    }

    public async Task DeleteLikeAsync(Guid likeId, CancellationToken cancellationToken)
    {
        var userPermissions = await _permissionsService
            .GetUserPermissionsAsync("like", UserId, likeId, cancellationToken);
        
        if(!userPermissions.Contains(PermissionTypes.Delete))
        {
            throw new NoPermissionException("Вы не можете убрать лайк, который вам не принадлежит.");
        }
        
        var likeEntity = await _unitOfWork.Likes.GetByIdAsync(likeId, cancellationToken);

        if (likeEntity == null)
        {
            throw new NotFoundException("Такого лайка не существует.");
        }
        
        await _unitOfWork.Likes.DeleteAsync(likeEntity, cancellationToken);
    }
}