using InformationPlatform.Application.Models;
using InformationPlatform.Application.Models.Requests;

namespace InformationPlatform.Application.Business.Interfaces;

public interface IMessagesBusinessService
{
    Task<List<MessageDto>> GetMessagesByChatIdAsync(Guid chatId, CancellationToken cancellationToken);
    Task AddMessageAsync(SendMessageRequest request, CancellationToken cancellationToken);
    Task DeleteMessagesAsync(List<Guid> messageIds, CancellationToken cancellationToken);
}