using Azure;
using DomainLayer.Exceptions;
using Microsoft.AspNetCore.Http;
using Shared.ErrorModels;
using System.Net;
using System.Text.Json;

namespace E_Commerace.Wep.CustamMiddelWares
{
    public class CustamExceptionHandlerMiddelWare
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustamExceptionHandlerMiddelWare> _logger;

        public CustamExceptionHandlerMiddelWare(RequestDelegate Next , ILogger<CustamExceptionHandlerMiddelWare> logger)
        {
            _next = Next;
            this._logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next.Invoke(httpContext);

                await HandleNotFoundEndPointAsync(httpContext);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "SomeThing Wrong");
                await HandleExceptionAsync(httpContext, ex);
            }

        }

        private static async Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {
            var Response = new ErrorToReturn()
            {
                StatusCode = httpContext.Response.StatusCode,
                ErrorMessage = ex.Message
            };
            httpContext.Response.StatusCode = ex switch
            {
                NotFoundException => StatusCodes.Status404NotFound,
                UnauthorizedAccessException=>StatusCodes.Status401Unauthorized,
                BadRequestException badRequestException=> GetBadRequestErrors(badRequestException, Response),
                _ => StatusCodes.Status500InternalServerError
            };

            /*var Response = new ErrorToReturn()
            {
                StatusCode = httpContext.Response.StatusCode,
                ErrorMessage = ex.Message
            };*/

            await httpContext.Response.WriteAsJsonAsync(Response);
        }

        private static int GetBadRequestErrors(BadRequestException badRequestException, ErrorToReturn? response)
        {
            response.Errors = badRequestException.Errors;
            return StatusCodes.Status400BadRequest;
        }

        private static async Task HandleNotFoundEndPointAsync(HttpContext httpContext)
        {
            if (httpContext.Response.StatusCode == StatusCodes.Status404NotFound)
            {
                var Response = new ErrorToReturn()
                {
                    StatusCode = StatusCodes.Status404NotFound,                 
                    ErrorMessage = $"End Point{httpContext.Request.Path}is not Found"
                };

                await httpContext.Response.WriteAsJsonAsync(Response);
            }
        }
    }
}
