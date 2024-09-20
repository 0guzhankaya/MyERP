using MyERP.Core.Models;
using MyERP.Core.Repositories;
using MyERP.Core.Services;
using MyERP.Core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyERP.Service.Services
{
    public class SaleService : GenericService<Sale>, ISaleService
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IProductService _productService;


        public SaleService(IGenericRepository<Sale> repository, IUnitOfWorks unitOfWorks, ISaleRepository saleRepository, IProductService productService) : base(repository, unitOfWorks)
        {
            _saleRepository = saleRepository;
            _productService = productService;
        }

        public async Task SaleProduct(Sale sale)
        {
            var product = await _productService.GetByIdAsync(sale.ProductId);

            product.Stock -= sale.Quantity;

            _productService.Update(product);

            sale.TotalPrice = sale.UnitPrice * sale.Quantity;

            await AddAsync(sale);
        }
    }
}
