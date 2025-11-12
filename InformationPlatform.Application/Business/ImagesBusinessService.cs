using AutoMapper;
using InformationPlatform.Application.Business.Interfaces;
using InformationPlatform.Application.Models;
using InformationPlatform.Domain.Models;
using InformationPlatform.Repository;

namespace InformationPlatform.Application.Business;

public class ImagesBusinessService : IImagesBusinessService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ImagesBusinessService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<DbImage>> AddImagesAsync(List<string> dataList, CancellationToken cancellationToken)
    {
        List<DbImage> response = [];
        
        foreach (var data in dataList)
        {
            var imageEntity = new DbImage()
            {
                Data = data
            };
        
            await _unitOfWork.Images.AddAsync(imageEntity, cancellationToken);

            response.Add(imageEntity);
        }

        return response;
    }

    public async Task DeleteImagesAsync(List<Guid> ids, CancellationToken cancellationToken)
    {
        var imageEntities = await _unitOfWork.Images
            .GetAsync(x => ids.Contains(x.Id), cancellationToken);
        
        await _unitOfWork.Images.DeleteRangeAsync(imageEntities, cancellationToken);
    }
}