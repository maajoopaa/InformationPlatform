namespace InformationPlatform.Application.Models;

public class LikeDto
{
    public Guid Id { get; set; }
    
    public UserDto CreatedBy { get; set; } = null!;
    
    public DateTime CreatedAt { get; set; }
}