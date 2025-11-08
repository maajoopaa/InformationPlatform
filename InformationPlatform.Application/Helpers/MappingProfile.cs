using AutoMapper;
using InformationPlatform.Application.Models;
using InformationPlatform.Domain.Models;

namespace InformationPlatform.Application.Helpers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<DbUser, UserDto>()
            .ForMember(x => x.UserSettings, x => x.MapFrom(y => y.UserSettings));
        
        CreateMap<DbChat, ChatDto>()
            .ForMember(x => x.Participants, x => x.MapFrom(y => y.Participants))
            .ForMember(x => x.Messages, x => x.MapFrom(y => y.Messages));

        CreateMap<DbComment, CommentDto>()
            .ForMember(x => x.CreatedBy, x => x.MapFrom(y => y.CreatedBy));

        CreateMap<DbImage,ImageDto>();
        
        CreateMap<DbLike, LikeDto>()
            .ForMember(x => x.CreatedBy, x => x.MapFrom(y => y.CreatedBy));
        
        CreateMap<DbMessage, MessageDto>()
            .ForMember(x => x.CreatedBy, x => x.MapFrom(y => y.CreatedBy))
            .ForMember(x => x.Images, x => x.MapFrom(y => y.Images));
        
        CreateMap<DbPost,PostDto>()
            .ForMember(x => x.Images,x => x.MapFrom(y => y.Images))
            .ForMember(x => x.Likes, x => x.MapFrom(y => y.Likes))
            .ForMember(x => x.Comments, x => x.MapFrom(y => y.Comments))
            .ForMember(x => x.CreatedBy, x => x.MapFrom(y => y.CreatedBy));

        CreateMap<DbUserSettings, UserSettingsDto>();
    }
}