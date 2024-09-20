using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyERP.API.Filters;
using MyERP.Core.DTOs;
using MyERP.Core.DTOs.UpdateDTOs;
using MyERP.Core.Models;
using MyERP.Core.Services;

namespace MyERP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : CustomBaseController
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductsController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            // url/api/products

            var product = _productService.GetAll();
            var dtos = _mapper.Map<List<ProductDto>>(product).OrderBy(x => x.Name).ToList();

            return CreateActionResult(CustomResponseDto<List<ProductDto>>.Success(200, dtos));

            // pegination will add.
        }

        [ServiceFilter(typeof(NotFoundFilter<Product>))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            // url/api/products/{id}

            var product = await _productService.GetByIdAsync(id);
            var productDto = _mapper.Map<ProductDto>(product);

            return CreateActionResult(CustomResponseDto<ProductDto>.Success(200, productDto));
        }

        [ServiceFilter(typeof(NotFoundFilter<Product>))]
        [HttpGet("[action]")]
        public async Task<IActionResult> Remove(int id)
        {
            // get user from token
            int userId = 1;

            var product = await _productService.GetByIdAsync(id);
            product.UpdatedBy = userId;

            _productService.ChangeStatus(product);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(404, "Not Found :("));
        }

        [HttpPost]
        public async Task<ActionResult> Save(ProductDto productDto)
        {
            // get user from token
            int userId = 1; 

            var processedEntity = _mapper.Map<Product>(productDto);
            processedEntity.UpdatedBy = userId;
            processedEntity.CreatedBy = userId;

            var product = await _productService.AddAsync(processedEntity);
            var productResponseDto = _mapper.Map<ProductDto>(product);

            // Kaynağın başarıyla oluşturulduğunu göstermek için CreatedAtAction kullanılır
            return CreatedAtAction(nameof(GetById), new { id = product.Id }, CustomResponseDto<ProductDto>.Success(201, productResponseDto));
        }

        public async Task<IActionResult> Update(ProductUpdateDto productDto)
        {
            // get user from token
            var userId = 1;

            var currentProduct = await _productService.GetByIdAsync(productDto.Id);

            currentProduct.UpdatedBy = userId;
            currentProduct.CreatedBy = userId;

            _productService.Update(currentProduct);

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }
    }
}
