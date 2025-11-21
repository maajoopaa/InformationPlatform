namespace InformationPlatform.Application.Models;

public class PostDto
{
    public Guid Id { get; set; }
    
    public string Title { get; set; } = null!;

    public string BodyHtml { get; set; } = null!;

    public List<ImageDto> Images { get; set; } = [];
    
    public List<LikeDto> Likes { get; set; } = [];
    
    public List<CommentDto> Comments { get; set; } = [];

    public UserDto CreatedBy { get; set; } = null!;
}