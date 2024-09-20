using MyERP.Core.Models;
using MyERP.Core.Repositories;
using MyERP.Core.Services;
using MyERP.Core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyERP.Service.Services
{
    public class ProductService : GenericService<Product>, IProductService
    {
        private readonly IProductRepository _productRepository;
        public ProductService(IGenericRepository<Product> repository, IUnitOfWorks unitOfWorks, IProductRepository productRepository) : base(repository, unitOfWorks)
        {
            _productRepository = productRepository;
        }

        public async Task BuyProduct(Product product)
        {
            var currentProduct = await _productRepository.GetByIdAsync(product.Id);

            currentProduct.Stock += product.Stock;

            Update(currentProduct);
        }
    }
}
