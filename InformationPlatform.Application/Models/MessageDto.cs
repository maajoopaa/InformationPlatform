namespace InformationPlatform.Application.Models;

public class MessageDto
{
    public Guid Id { get; set; }
    
    public string BodyHtml { get; set; } = null!;
    
    public UserDto CreatedBy { get; set; } = null!;
    
    public DateTime CreatedAt { get; set; }

    public List<ImageDto> Images { get; set; } = [];
}