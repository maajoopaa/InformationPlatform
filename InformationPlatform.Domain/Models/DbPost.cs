using Templates;

namespace InformationPlatform.Domain.Models;

public class DbPost : BaseDbEntityWithId
{
    public string Title { get; set; } = null!;

    public string BodyHtml { get; set; } = null!;
    
    public DateTime CreatedAt { get; set; }

    public virtual List<DbImage> Images { get; set; } = [];
    
    public virtual List<DbLike> Likes { get; set; } = [];
    
    public virtual List<DbComment> Comments { get; set; } = [];

    public Guid CreatedById { get; set; }
    
    public virtual DbUser CreatedBy { get; set; } = null!;
}