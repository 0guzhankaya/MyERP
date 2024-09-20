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
    public class PaymentsController : CustomBaseController
    {
        private readonly IPaymentService _paymentService;
        private readonly IMapper _mapper;

        public PaymentsController(IPaymentService paymentService, IMapper mapper)
        {
            _paymentService = paymentService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            // url/api/payments

            var payments = _paymentService.GetAll();
            var dtos = _mapper.Map<List<PaymentDto>>(payments).OrderBy(x => x.Amount).ToList();

            return CreateActionResult(CustomResponseDto<List<PaymentDto>>.Success(200, dtos));

            // pagination will add.
        }


        [ServiceFilter(typeof(NotFoundFilter<Payment>))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            // url/api/payments/{id}

            var payment = await _paymentService.GetByIdAsync(id);
            var paymentDto = _mapper.Map<PaymentDto>(payment);

            return CreateActionResult(CustomResponseDto<PaymentDto>.Success(200, paymentDto));
        }

        [HttpPost]
        public async Task<IActionResult> Save(PaymentDto paymentDto)
        {
            // get user from token
            int userId = 1;

            var processedEntity = _mapper.Map<Payment>(paymentDto);

            var payment = await _paymentService.AddAsync(processedEntity);
            var paymentResponseDto = _mapper.Map<PaymentDto>(payment);

            return CreateActionResult(CustomResponseDto<PaymentDto>.Success(201, paymentDto));
        }

        [HttpPut]
        public async Task<IActionResult> Update(PaymentUpdateDto paymentDto)
        {
            // get user from token
            var userId = 1;

            var currentPayment = await _paymentService.GetByIdAsync(paymentDto.Id);
            currentPayment.UpdatedBy = userId;
            currentPayment.Amount = paymentDto.Amount;

            _paymentService.Update(currentPayment);

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }
    }
}
