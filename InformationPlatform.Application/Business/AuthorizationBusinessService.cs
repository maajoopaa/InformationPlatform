using AutoMapper;
using InformationPlatform.Application.Business.Interfaces;
using InformationPlatform.Application.Exceptions;
using InformationPlatform.Application.Helpers;
using InformationPlatform.Application.Models;
using InformationPlatform.Application.Models.Requests;
using InformationPlatform.Domain.Models;
using InformationPlatform.Repository;

namespace InformationPlatform.Application.Business;

public class AuthorizationBusinessService : IAuthorizationBusinessService
{
    private readonly JWTHelper _jwtHelper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public AuthorizationBusinessService(JWTHelper jwtHelper,IUnitOfWork unitOfWork, IMapper mapper)
    {
        _jwtHelper = jwtHelper;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    public async Task<AuthorizationResponse> LoginAsync(LoginRequest request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.Users.GetByUsernameAsync(request.Username, cancellationToken);

        if (user == null || !PasswordHasher.VerifyPassword(user.PasswordHash, request.Password))
        {
            throw new NotFoundException("Такого пользователя не существует.");
        }

        var token = _jwtHelper.GenerateToken(user.Id,user.Username);

        return new AuthorizationResponse
        {
            Token = token,
            User = _mapper.Map<UserDto>(user)
        };
    }

    public async Task<AuthorizationResponse> RegisterAsync(CreateUserRequest request, CancellationToken cancellationToken)
    {
        var userEntity = _mapper.Map<DbUser>(request);
        userEntity.PasswordHash = PasswordHasher.HashPassword(request.Password);
        userEntity.DateOfCreation = DateTime.UtcNow;
        userEntity.LastLogin = DateTime.UtcNow;

        var userSettings = new DbUserSettings
        {
            Theme = Themes.Dark
        };

        await _unitOfWork.UserSettings.AddAsync(userSettings, cancellationToken);

        userEntity.UserSettingsId = userSettings.Id;
        
        try
        {
            await _unitOfWork.Users.AddAsync(userEntity,cancellationToken);
        }
        catch (Exception ex)
        {
            throw new NotFoundException("Такой пользователь уже существует.");
        }

        return await LoginAsync(new LoginRequest
        {
            Username = request.Username,
            Password = request.Password,
        }, cancellationToken);
    }
}