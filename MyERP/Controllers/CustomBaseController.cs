using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyERP.Core.DTOs;

namespace MyERP.API.Controllers
{
    public class CustomBaseController : ControllerBase
    {
        [NonAction] // Abstraction
        public IActionResult CreateActionResult<T>(CustomResponseDto<T> response)
        {
            if (response.StatusCode == 204)
            {
                // 204 : NoContent
                return new ObjectResult(null)
                {
                    StatusCode = response.StatusCode
                };
            }
            return new ObjectResult(response) { StatusCode = response.StatusCode };
        }
    }
}
