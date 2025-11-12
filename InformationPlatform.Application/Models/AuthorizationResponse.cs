namespace InformationPlatform.Application.Models;

public class AuthorizationResponse
{
    public string? Token { get; set; }
    
    public UserDto? User { get; set; }
    
}