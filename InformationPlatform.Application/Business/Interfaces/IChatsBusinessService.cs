using InformationPlatform.Application.Models;
using InformationPlatform.Application.Models.Requests;
using Templates.Business;

namespace InformationPlatform.Application.Business.Interfaces;

public interface IChatsBusinessService : IBaseBusinessService
{
    Task<List<ChatDto>> GetChatsByUserIdAsync(Guid userId, CancellationToken cancellationToken);
    Task<ChatDto> AddChatAsync(CreateChatRequest request, CancellationToken cancellationToken);
    Task UpdateChatAsync(Guid chatId, UpdateChatRequest request, CancellationToken cancellationToken);
    Task DeleteChatsAsync(List<Guid> ids, CancellationToken cancellationToken);
    Task AddParticipantsAsync(Guid chatId, List<Guid> participantIds, CancellationToken cancellationToken);
}