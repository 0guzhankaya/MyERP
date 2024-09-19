using Microsoft.AspNetCore.Diagnostics;
using MyERP.Core.DTOs;
using MyERP.Service.Exceptions;
using System.Text.Json;

namespace MyERP.API.Middlewares
{
    public static class CustomExceptionMiddleware
    {
        public static void UseCustomException(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(config =>
            {
                config.Run(async context =>
                {
                    context.Response.ContentType = "application/json";

                    var exceptionFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (exceptionFeature != null)
                    {
                        var statusCode = exceptionFeature.Error switch
                        {
                            ClientSideException => 400,
                            NotFoundException => 404,
                            _ => 500,
                        };

                        context.Response.StatusCode = statusCode;

                        var response = CustomResponseDto<NoContentDto>.Fail(statusCode, exceptionFeature.Error.Message);

                        var responseBody = JsonSerializer.Serialize(response);

                        await context.Response.WriteAsync(responseBody);
                    }
                });
            });
        }
    }
}
