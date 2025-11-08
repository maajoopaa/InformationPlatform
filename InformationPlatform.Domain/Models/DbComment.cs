using Templates;

namespace InformationPlatform.Domain.Models;

public class DbComment : BaseDbEntityWithId
{
    public string Text { get; set; } = null!;

    public Guid PostId { get; set; }
    
    public virtual DbPost Post { get; set; } = null!;

    public Guid CreatedById { get; set; }
    
    public virtual DbUser CreatedBy { get; set; } = null!;
    
    public DateTime CreatedAt { get; set; }
}