namespace InformationPlatform.Application.Models;

public class CommentDto
{
    public string Text { get; set; } = null!;

    public UserDto CreatedBy { get; set; } = null!;
    
    public DateTime CreatedAt { get; set; }
}