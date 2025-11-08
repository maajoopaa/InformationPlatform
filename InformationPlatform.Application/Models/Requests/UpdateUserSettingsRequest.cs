using InformationPlatform.Domain.Models;

namespace InformationPlatform.Application.Models.Requests;

public class UpdateUserSettingsRequest
{
    public Themes Theme { get; set; }
}