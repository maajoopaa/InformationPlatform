using InformationPlatform.Application.Helpers.Interfaces;
using InformationPlatform.Domain.Models;
using InformationPlatform.Repository;
using Npgsql.EntityFrameworkCore.PostgreSQL.Query.Internal;

namespace InformationPlatform.Application.Helpers;

public class PermissionsService : IPermissionsService
{
    private readonly IUnitOfWork _unitOfWork;

    private readonly List<PermissionTypes> _allPermissionsList =
        [PermissionTypes.Read, PermissionTypes.Write, PermissionTypes.Delete];

    public PermissionsService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<PermissionTypes>> GetUserPermissionsAsync(string resourceType, Guid userId, Guid resourceId, CancellationToken cancellationToken)
    {
        switch (resourceType.ToLower())
        {
            case "chat":
                var chat = await _unitOfWork.Chats.GetByIdAsync(resourceId,cancellationToken);

                if (chat?.Participants.FirstOrDefault(x => x.Id == userId) != null)
                {
                    return _allPermissionsList;
                }

                return [];
            case "comment":
                var comment = await _unitOfWork.Comments.GetByIdAsync(resourceId, cancellationToken);

                if (comment?.CreatedById == userId)
                {
                    return _allPermissionsList;
                }

                return [PermissionTypes.Read];
            case "like":
                var like = await _unitOfWork.Likes.GetByIdAsync(resourceId, cancellationToken);

                if (like?.CreatedById == userId)
                {
                    return _allPermissionsList;
                }

                return [PermissionTypes.Read];
            case "message":
                var message = await _unitOfWork.Messages.GetByIdAsync(resourceId, cancellationToken);

                if (message?.CreatedById == userId)
                {
                    return _allPermissionsList;
                }

                var messageChat = await _unitOfWork.Chats
                    .GetByIdAsync(message?.ChatId ?? Guid.NewGuid(), cancellationToken);

                if (messageChat?.Participants.FirstOrDefault(x => x.Id == userId) != null)
                {
                    return [PermissionTypes.Read];
                }

                return [];
            case "post":
                var post = await _unitOfWork.Posts.GetByIdAsync(resourceId, cancellationToken);

                if (post?.CreatedById == userId)
                {
                    return _allPermissionsList;
                }

                return [PermissionTypes.Read];
            case "user":
                if (resourceId == userId)
                {
                    return  _allPermissionsList;
                }

                return [PermissionTypes.Read];
            default:
                return [];
        }
    }
}