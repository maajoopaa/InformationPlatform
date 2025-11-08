using Templates;

namespace InformationPlatform.Domain.Models;

public class DbImage : BaseDbEntityWithId
{
    public string Data { get; set; } = null!;
}