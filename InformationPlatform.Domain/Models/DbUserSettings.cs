using Templates;

namespace InformationPlatform.Domain.Models;

public class DbUserSettings : BaseDbEntityWithId
{
    public Themes Theme { get; set; }
}