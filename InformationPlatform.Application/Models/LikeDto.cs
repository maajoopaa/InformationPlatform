namespace InformationPlatform.Application.Models;

public class LikeDto
{
    public UserDto CreatedBy { get; set; } = null!;
    
    public DateTime CreatedAt { get; set; }
}