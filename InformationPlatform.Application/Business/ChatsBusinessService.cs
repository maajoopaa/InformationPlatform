using AutoMapper;
using InformationPlatform.Application.Business.Interfaces;
using InformationPlatform.Application.Exceptions;
using InformationPlatform.Application.Models;
using InformationPlatform.Application.Models.Requests;
using InformationPlatform.Domain.Models;
using InformationPlatform.Repository;

namespace InformationPlatform.Application.Business;

public class ChatsBusinessService : IChatsBusinessService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ChatsBusinessService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    public async Task<List<ChatDto>> GetChatsByUserIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        var chatEntities = await _unitOfWork.Chats
            .GetAsync(x => x.Participants.Any(y => y.Id == userId), cancellationToken);

        return _mapper.Map<List<ChatDto>>(chatEntities);
    }

    public async Task AddChatAsync(CreateChatRequest request, CancellationToken cancellationToken)
    {
        var chat = new DbChat
        {
            Title = request.Title,
            IsGroup = request.IsGroup
        };

        var participants = await _unitOfWork.Users.GetAsync(
            u => request.ParticipantIds.Contains(u.Id),
            cancellationToken
        );
        
        chat.Participants.AddRange(participants);
        
        await _unitOfWork.Chats.AddAsync(chat, cancellationToken);
    }

    public async Task UpdateChatAsync(Guid chatId, UpdateChatRequest request, CancellationToken cancellationToken)
    {
        var chatEntity = await _unitOfWork.Chats.GetByIdAsync(chatId, cancellationToken);

        if (chatEntity == null)
        {
            throw new NotFoundException("Такого чата не существует.");
        }
        
        chatEntity.Title = request.Title;
        chatEntity.IsGroup = request.IsGroup;
        
        await _unitOfWork.Chats.UpdateAsync(chatEntity, cancellationToken);
    }

    public async Task DeleteChatsAsync(List<Guid> ids, CancellationToken cancellationToken)
    {
        var chatEntities = await _unitOfWork.Chats
            .GetAsync(x => ids.Contains(x.Id), cancellationToken);
        
        await _unitOfWork.Chats.DeleteRangeAsync(chatEntities, cancellationToken);
    }

    public async Task AddParticipantsAsync(Guid chatId, List<Guid> participantIds, CancellationToken cancellationToken)
    {
        var chatEntity = await _unitOfWork.Chats.GetByIdAsync(chatId, cancellationToken);

        if (chatEntity == null)
        {
            throw new NotFoundException("Такого чата не существует.");
        }
        
        var participants = await _unitOfWork.Users.GetAsync(
            u => participantIds.Contains(u.Id),
            cancellationToken
        );
        
        chatEntity.Participants.AddRange(participants);

        await _unitOfWork.Chats.UpdateAsync(chatEntity, cancellationToken);
    }
}