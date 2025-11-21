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

public class MessagesBusinessService :BaseBusinessService, IMessagesBusinessService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IImagesBusinessService _imagesBusinessService;
    private readonly IPermissionsService _permissionsService;

    public MessagesBusinessService(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IImagesBusinessService imagesBusinessService,
        IHttpContextAccessor httpContextAccessor,
        IPermissionsService permissionsService) : base(httpContextAccessor)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _imagesBusinessService = imagesBusinessService;
        _permissionsService = permissionsService;
    }
    
    public async Task<List<MessageDto>> GetMessagesByChatIdAsync(Guid chatId, CancellationToken cancellationToken)
    {
        var userPermissions = await _permissionsService
            .GetUserPermissionsAsync("chat", UserId, chatId, cancellationToken);
        
        if(!userPermissions.Contains(PermissionTypes.Read))
        {
            throw new NoPermissionException("Вы не можете просмотреть сообщения чата, в котором не состоите.");
        }
        
        var messageEntities = await _unitOfWork.Messages
            .GetAsync(x => x.ChatId == chatId, cancellationToken);
        
        return _mapper.Map<List<MessageDto>>(messageEntities);
    }

    public async Task<MessageDto> AddMessageAsync(SendMessageRequest request, CancellationToken cancellationToken)
    {
        var userPermissions = await _permissionsService
            .GetUserPermissionsAsync("chat", UserId, request.ChatId, cancellationToken);
        
        if(!userPermissions.Contains(PermissionTypes.Write))
        {
            throw new NoPermissionException("Вы не можете добавить сообщение в чат, в котором не состоите.");
        }
        
        var chatEntity = await _unitOfWork.Chats.GetByIdAsync(request.ChatId, cancellationToken);

        if (chatEntity == null)
        {
            throw new NotFoundException("Такого чата не существует.");
        }

        var messageEntity = new DbMessage
        {
            BodyHtml = request.BodyHtml,
            ChatId = request.ChatId,
            CreatedAt =  DateTime.UtcNow,
            CreatedById = UserId
        };

        var imageEntities = await _imagesBusinessService.AddImagesAsync(request.Images, cancellationToken);
        
        messageEntity.Images.AddRange(imageEntities);
        
        await _unitOfWork.Messages.AddAsync(messageEntity, cancellationToken);
        
        return _mapper.Map<MessageDto>(messageEntity);
    }

    public async Task DeleteMessagesAsync(List<Guid> messageIds, CancellationToken cancellationToken)
    {
        var messageEntities = await _unitOfWork.Messages
            .GetAsync(x =>  messageIds.Contains(x.Id), cancellationToken);

        foreach (var messageEntity in messageEntities)
        {
            var userPermissions = await _permissionsService
                .GetUserPermissionsAsync("message", UserId, messageEntity.Id, cancellationToken);
        
            if(!userPermissions.Contains(PermissionTypes.Delete))
            {
                throw new NoPermissionException("Вы не можете удалить сообщение, которое вам не приналежит.");
            }
        }
        
        await _unitOfWork.Messages.DeleteRangeAsync(messageEntities, cancellationToken);
    }
}