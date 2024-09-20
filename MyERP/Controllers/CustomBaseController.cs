using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyERP.Core.DTOs;
using System.IdentityModel.Tokens.Jwt;

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

        [NonAction] // Abstraction
        public int GetUserFromToken()
        {
            string requestHeader = Request.Headers["Authorization"];
            string jwt = requestHeader?.Replace("Bearer ", "");
            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadToken(jwt) as JwtSecurityToken;
            string userId = jwtSecurityToken.Claims.FirstOrDefault(claim => claim.Type == "sub")?.Value;
            int id = Int32.Parse(userId);
            return id == 0 ? 0 : id;
        }
    }
}
