using CamplyMarket.Application.Abstraction.Storage;
using CamplyMarket.Application.Repositories.ProductImageFileInterface;
using CamplyMarket.Application.Repositories.ProductInterface;
using CamplyMarket.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CamplyMarket.Application.Features.Commands.ProductImageFile.UploadProductImage;
public class UploadProductImageCommandHandler : IRequestHandler<UploadProductImageCommandRequest, UploadProductImageCommandResponse>
{
    readonly IStorageService _storage;
    readonly IProductReadRepository _productReadRepository;
    readonly IProductImageFileWriteRepository _productImageFileWrite;

    public UploadProductImageCommandHandler(IProductReadRepository productReadRepository, IProductImageFileWriteRepository productImageFileWrite, IStorageService storage)
    {
        _productReadRepository = productReadRepository;
        _productImageFileWrite = productImageFileWrite;
        _storage = storage;
    }

    public async Task<UploadProductImageCommandResponse> Handle(UploadProductImageCommandRequest request, CancellationToken cancellationToken)
    {
        List<(string fileName, string pathOrContainerName)> result = await _storage.UploadAsync("product-images", request.Files);

        CamplyMarket.Domain.Entities. Product product = await _productReadRepository.GetByIdAsync(request.id);

        await _productImageFileWrite.AddRangeAsync(result.Select(d => new ProductImageFiles()
        {
            FileName = d.fileName,
            Path = d.pathOrContainerName,
            Storage = _storage.StorageName,
            Products = new List<CamplyMarket.Domain.Entities.Product>() { product }

        }).ToList());
        await _productImageFileWrite.SaveAsync();
        return new();
    }
}
