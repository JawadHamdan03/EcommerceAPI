using Ecommerce.BLL.Services.Interfaces;
using Ecommerce.DAL.DTO.Requests;
using Ecommerce.DAL.DTO.Responses;
using Ecommerce.DAL.Models;
using Ecommerce.DAL.Repositery.Interfaces;
using Mapster;
using System;
using System.Threading.Tasks;

namespace Ecommerce.BLL.Services.Classes
{
    public class ProductService
        : GenericService<ProductRequest, ProductReposne, Product>, IProductService
    {
        private readonly IProductRepositery _productRepositery;
        private readonly IFileService _fileService;

        public ProductService(IProductRepositery productRepositery, IFileService fileService)
            : base(productRepositery)
        {
            _productRepositery = productRepositery ?? throw new ArgumentNullException(nameof(productRepositery));
            _fileService = fileService ?? throw new ArgumentNullException(nameof(fileService));
        }

        public async Task<bool> CreateFileAsync(ProductRequest request)
        {
            if (request is null) throw new ArgumentNullException(nameof(request));

            var entity = request.Adapt<Product>();
            entity.CreatedAt = DateTime.UtcNow;

            if (request.MainImage != null)
            {
                
                var imagePath = await _fileService.UploadFile(request.MainImage);
                entity.MainImage = imagePath;
            }

            return _productRepositery.Create(entity);
        }
    }
}
