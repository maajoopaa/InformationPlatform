namespace InformationPlatform.Application.Models.Requests;

public class UpdateChatRequest
{
    public string? Title { get; set; }

    public bool IsGroup { get; set; } = false;
    
    public virtual List<Guid> ParticipantIds { get; set; } = [];
}