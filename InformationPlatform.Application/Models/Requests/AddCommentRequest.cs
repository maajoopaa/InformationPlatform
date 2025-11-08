namespace InformationPlatform.Application.Models.Requests;

public class AddCommentRequest
{
    public string Text { get; set; } = null!;

    public Guid PostId { get; set; }
}