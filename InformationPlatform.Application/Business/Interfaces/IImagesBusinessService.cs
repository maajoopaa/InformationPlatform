using InformationPlatform.Application.Models;
using InformationPlatform.Domain.Models;
using Templates.Business;

namespace InformationPlatform.Application.Business.Interfaces;

public interface IImagesBusinessService : IBaseBusinessService
{
    Task<List<DbImage>> AddImagesAsync(List<string> dataList, CancellationToken cancellationToken);
    Task DeleteImagesAsync(List<Guid> ids, CancellationToken cancellationToken);
}