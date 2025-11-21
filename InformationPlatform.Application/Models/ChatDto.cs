namespace InformationPlatform.Application.Models;

public class ChatDto
{
    public Guid Id { get; set; }
    
    public string? Title { get; set; }

    public bool IsGroup { get; set; } = false;
    
    public List<UserDto> Participants { get; set; } = [];

    public List<MessageDto> Messages { get; set; } = [];
}