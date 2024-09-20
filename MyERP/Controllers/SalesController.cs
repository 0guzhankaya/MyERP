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
    public class SalesController : CustomBaseController
    {
        private readonly ISaleService _saleService;
        private readonly IMapper _mapper;

        public SalesController(ISaleService saleService, IMapper mapper)
        {
            _saleService = saleService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            // url/api/sales

            var sales = _saleService.GetAll();
            var dtos = _mapper.Map<List<SaleDto>>(sales).OrderBy(x => x.TotalPrice).ToList();

            return CreateActionResult(CustomResponseDto<List<SaleDto>>.Success(200, dtos));

            // pagination will add.
        }

        [ServiceFilter(typeof(NotFoundFilter<Sale>))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            // url/api/sales/{id}

            var sales = await _saleService.GetByIdAsync(id);
            var saleDto = _mapper.Map<SaleDto>(sales);

            return CreateActionResult(CustomResponseDto<SaleDto>.Success(200, saleDto));
        }

        [ServiceFilter(typeof(NotFoundFilter<Sale>))]
        [HttpGet("[action]")]
        public async Task<IActionResult> Remove(int id)
        {
            // get user from token
            int userId = 1;

            var sale = await _saleService.GetByIdAsync(id);
            sale.UpdatedBy = userId;

            _saleService.ChangeStatus(sale);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(404, "Not Found :("));
        }

        [HttpPost]
        public async Task<IActionResult> Save(SaleDto saleDto)
        {
            // get user from token
            int userId = 1;

            var processedEntity = _mapper.Map<Sale>(saleDto);

            processedEntity.UpdatedBy = userId;
            processedEntity.CreatedBy = userId;

            var sale = await _saleService.AddAsync(processedEntity);
            var saleResponseDto = _mapper.Map<SaleDto>(sale);

            return CreateActionResult(CustomResponseDto<SaleDto>.Success(201, saleDto));
        }

        [HttpPut]
        public async Task<IActionResult> Update(SaleUpdateDto saleDto)
        {
            // get user from token
            var userId = 1;

            var currentSale = await _saleService.GetByIdAsync(saleDto.Id);

            currentSale.UpdatedBy = userId;
            currentSale.Quantity = saleDto.Quantity;
            currentSale.ProductId = saleDto.ProductId;

            _saleService.Update(currentSale);

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }
    }
}
