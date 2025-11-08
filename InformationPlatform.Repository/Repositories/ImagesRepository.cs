using InformationPlatform.Database;
using InformationPlatform.Domain.Models;
using InformationPlatform.Repository.Repositories.Interfaces;
using Templates.Repositories;

namespace InformationPlatform.Repository.Repositories;

public class ImagesRepository : BaseRepository<DbImage,InformationPlatformDbContext>, IImagesRepository
{
    public ImagesRepository(InformationPlatformDbContext context) 
        : base(context) { }
}