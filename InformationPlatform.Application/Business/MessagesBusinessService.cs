using AutoMapper;
using InformationPlatform.Application.Business.Interfaces;
using InformationPlatform.Application.Exceptions;
using InformationPlatform.Application.Models;
using InformationPlatform.Application.Models.Requests;
using InformationPlatform.Domain.Models;
using InformationPlatform.Repository;

namespace InformationPlatform.Application.Business;

public class MessagesBusinessService : IMessagesBusinessService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IImagesBusinessService _imagesBusinessService;

    public MessagesBusinessService(IUnitOfWork unitOfWork,IMapper mapper, IImagesBusinessService imagesBusinessService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _imagesBusinessService = imagesBusinessService;
    }
    
    public async Task<List<MessageDto>> GetMessagesByChatIdAsync(Guid chatId, CancellationToken cancellationToken)
    {
        var messageEntities = await _unitOfWork.Messages
            .GetAsync(x => x.ChatId == chatId, cancellationToken);
        
        return _mapper.Map<List<MessageDto>>(messageEntities);
    }

    public async Task AddMessageAsync(SendMessageRequest request, CancellationToken cancellationToken)
    {
        var chatEntity = await _unitOfWork.Chats.GetByIdAsync(request.ChatId, cancellationToken);

        if (chatEntity == null)
        {
            throw new NotFoundException("Такого чата не существует.");
        }

        var messageEntity = new DbMessage
        {
            BodyHtml = request.BodyHtml,
            ChatId = request.ChatId,
            CreatedAt =  DateTime.UtcNow
        };

        var imageEntities = await _imagesBusinessService.AddImagesAsync(request.Images, cancellationToken);
        
        messageEntity.Images.AddRange(imageEntities);
        
        await _unitOfWork.Messages.AddAsync(messageEntity, cancellationToken);
    }

    public async Task DeleteMessagesAsync(List<Guid> messageIds, CancellationToken cancellationToken)
    {
        var messageEntities = await _unitOfWork.Messages
            .GetAsync(x =>  messageIds.Contains(x.Id), cancellationToken);
        
        await _unitOfWork.Messages.DeleteRangeAsync(messageEntities, cancellationToken);
    }
}