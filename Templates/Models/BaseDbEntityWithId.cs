using System.ComponentModel.DataAnnotations;

namespace Templates;

public class BaseDbEntityWithId
{
    [Key]
    public Guid Id { get; set; }
}