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

public class PostsBusinessService :BaseBusinessService, IPostsBusinessService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IImagesBusinessService _imagesBusinessService;
    private readonly IPermissionsService _permissionsService;

    public PostsBusinessService(
        IUnitOfWork unitOfWork, 
        IMapper mapper, IImagesBusinessService imagesBusinessService,
        IHttpContextAccessor httpContextAccessor,
        IPermissionsService permissionsService) : base(httpContextAccessor)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _imagesBusinessService = imagesBusinessService;
        _permissionsService = permissionsService;
    }
    
    public async Task<List<PostDto>> GetPostsByUserIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        var postEntities = await _unitOfWork.Posts
            .GetAsync(x => x.CreatedById == userId, cancellationToken);
        
        return _mapper.Map<List<PostDto>>(postEntities);
    }
    
    public async Task<List<PostDto>> GetAllPostsAsync(CancellationToken cancellationToken)
    {
        var postEntities = await _unitOfWork.Posts
            .GetAsync(null, cancellationToken);
        
        return _mapper.Map<List<PostDto>>(postEntities);
    }

    public async Task AddPostAsync(CreatePostRequest request, CancellationToken cancellationToken)
    {
        var postEntity = new DbPost
        {
            Title = request.Title,
            BodyHtml = request.BodyHtml
        };

        var imageEntities = await _imagesBusinessService.AddImagesAsync(request.Images, cancellationToken);
        
        postEntity.Images.AddRange(imageEntities);
        
        await _unitOfWork.Posts.AddAsync(postEntity, cancellationToken);
    }

    public async Task DeletePostAsync(Guid postId, CancellationToken cancellationToken)
    {
        var userPermissions = await _permissionsService
            .GetUserPermissionsAsync("post", UserId, postId, cancellationToken);
        
        if(!userPermissions.Contains(PermissionTypes.Delete))
        {
            throw new NoPermissionException("Вы не можете удалить пост, который вам не принадлежит.");
        }
        
        var postEntity = await _unitOfWork.Posts.GetByIdAsync(postId, cancellationToken);

        if (postEntity == null)
        {
            throw new NotFoundException("Такого поста не существует.");
        }
        
        await _unitOfWork.Posts.DeleteAsync(postEntity, cancellationToken);
    }
}