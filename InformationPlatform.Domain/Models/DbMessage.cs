using Templates;

namespace InformationPlatform.Domain.Models;

public class DbMessage : BaseDbEntityWithId
{
    public string BodyHtml { get; set; } = null!;
    
    public Guid ChatId { get; set; }

    public virtual DbChat Chat { get; set; } = null!;
    
    public Guid CreatedById { get; set; }

    public virtual DbUser CreatedBy { get; set; } = null!;
    
    public DateTime CreatedAt { get; set; }

    public virtual List<DbImage> Images { get; set; } = [];
}