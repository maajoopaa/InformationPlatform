namespace InformationPlatform.Application.Models.Requests;

public class CreatePostRequest
{
    public string Title { get; set; } = null!;

    public string BodyHtml { get; set; } = null!;

    public List<string> Images { get; set; } = [];
}