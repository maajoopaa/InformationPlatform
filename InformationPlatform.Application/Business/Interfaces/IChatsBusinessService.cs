using InformationPlatform.Application.Models;
using InformationPlatform.Application.Models.Requests;

namespace InformationPlatform.Application.Business.Interfaces;

public interface IChatsBusinessService
{
    Task<List<ChatDto>> GetChatsByUserIdAsync(Guid userId, CancellationToken cancellationToken);
    Task AddChatAsync(CreateChatRequest request, CancellationToken cancellationToken);
    Task UpdateChatAsync(Guid chatId, UpdateChatRequest request, CancellationToken cancellationToken);
    Task DeleteChatsAsync(List<Guid> ids, CancellationToken cancellationToken);
    Task AddParticipantsAsync(Guid chatId, List<Guid> participantIds, CancellationToken cancellationToken);
}