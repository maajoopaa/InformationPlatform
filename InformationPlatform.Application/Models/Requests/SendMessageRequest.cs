namespace InformationPlatform.Application.Models.Requests;

public class SendMessageRequest
{
    public string BodyHtml { get; set; } = null!;
    
    public Guid ChatId { get; set; }

    public List<string> Images { get; set; } = [];
}