using InformationPlatform.Application.Models;
using InformationPlatform.Domain.Models;

namespace InformationPlatform.Application.Business.Interfaces;

public interface IImagesBusinessService
{
    Task<List<DbImage>> AddImagesAsync(List<string> dataList, CancellationToken cancellationToken);
    Task DeleteImagesAsync(List<Guid> ids, CancellationToken cancellationToken);
}