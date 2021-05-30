using Prueba.Tecnica.Domain.Entities;
using Prueba.Tecnica.Domain.Entities.Config;
using Prueba.Tecnica.Domain.Services.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace Prueba.Tecnica.WebApi.Middleware
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger logger;

        public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                logger.LogError($"-- Error: {ex.Message}  --- Stack Trace : {ex.StackTrace}");
                var response = context.Response;
                response.ContentType = "application/json";
                response.StatusCode = 404;
                ErrorResponse errorResponse = new ErrorResponse
                {
                    ResultCode = Constants.SHIP_POSITION_NOT_DETERMINED,
                    ResultMsg = Constants.SHIP_POSITION_NOT_DETERMINED_DESC
                };
                GenericResponse genericResponse = Helper.ManageResponse(errorResponse, false);
                var result = JsonSerializer.Serialize(genericResponse);
                await response.WriteAsync(result);
            }
        }
    }
}