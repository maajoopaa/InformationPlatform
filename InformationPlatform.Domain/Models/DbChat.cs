using Templates;

namespace InformationPlatform.Domain.Models;

public class DbChat : BaseDbEntityWithId
{
    public string? Title { get; set; }

    public bool IsGroup { get; set; }
    
    public virtual List<DbUser> Participants { get; set; } = [];

    public virtual List<DbMessage> Messages { get; set; } = [];
}