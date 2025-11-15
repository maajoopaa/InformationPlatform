using Templates;

namespace InformationPlatform.Domain.Models;

public class DbUser : BaseDbEntityWithId
{
    public string Username { get; set; } = null!;

    public string FirstName { get; set; } = null!;
    
    public string LastName { get; set; } = null!;
    
    public DateTime LastLogin { get; set; }
    
    public DateTime DateOfCreation { get; set; }
    
    public DateTime BirthDate { get; set; }

    public string PasswordHash { get; set; } = null!;

    public virtual List<DbPost> Posts { get; set; } = [];
    
    public virtual List<DbLike> Likes { get; set; } = [];
    
    public virtual List<DbComment> Comments { get; set; } = [];
    
    public virtual List<DbChat> Chats { get; set; } = [];
    
    public Guid UserSettingsId { get; set; }

    public virtual DbUserSettings UserSettings { get; set; } = null!;
}
