using InformationPlatform.Domain.Models;

namespace InformationPlatform.Application.Models.Requests;

public class CreateChatRequest
{
    public string? Title { get; set; }

    public bool IsGroup { get; set; } = false;
    
    public List<Guid> ParticipantIds { get; set; } = [];
}