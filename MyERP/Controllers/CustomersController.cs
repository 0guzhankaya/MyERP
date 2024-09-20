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
    public class CustomersController : CustomBaseController
    {
        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;

        public CustomersController(ICustomerService customerService, IMapper mapper)
        {
            _customerService = customerService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> All() 
        {
            // url/api/customers

            var customers = _customerService.GetAll();
            var dtos = _mapper.Map<List<CustomerDto>>(customers).OrderBy(x => x.Name).ToList();

            return CreateActionResult(CustomResponseDto<List<CustomerDto>>.Success(200, dtos));

            // pagination will add.
        }

        [ServiceFilter(typeof(NotFoundFilter<Customer>))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            // url/api/customers/{id}

            var customer = await _customerService.GetByIdAsync(id);
            var customerDto = _mapper.Map<CustomerDto>(customer);

            return CreateActionResult(CustomResponseDto<CustomerDto>.Success(200, customerDto));
        }

        [ServiceFilter(typeof(NotFoundFilter<Customer>))]
        [HttpGet("[action]")]
        public async Task<IActionResult> Remove(int id)
        {
            // get user from token
            int userId = 1;

            var customer = await _customerService.GetByIdAsync(id);
            customer.UpdatedBy = userId;

            _customerService.ChangeStatus(customer);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(404 , "Not Found :("));
        }

        [HttpPost]
        public async Task<IActionResult> Save(CustomerDto customerDto)
        {
            // get user from token
            int userId = 1;

            var processedEntity = _mapper.Map<Customer>(customerDto);
            
            processedEntity.UpdatedBy = userId;
            processedEntity.CreatedBy = userId;

            var customer = await _customerService.AddAsync(processedEntity);

            var customerResponseDto = _mapper.Map<CustomerDto>(customer);

            return CreateActionResult(CustomResponseDto<CustomerDto>.Success(201, customerDto));
        }

        [HttpPut]
        public async Task<IActionResult> Update(CustomerUpdateDto customerDto)
        {
            // get user from token
            var userId = 1;

            var currentCustomer = await _customerService.GetByIdAsync(customerDto.Id);

            currentCustomer.UpdatedBy = userId;
            currentCustomer.Name = customerDto.Name;

            _customerService.Update(currentCustomer);

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }
    }
}
