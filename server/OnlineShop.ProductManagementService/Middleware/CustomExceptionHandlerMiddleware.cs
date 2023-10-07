using AutoMapper.Configuration.Conventions;
using OnlineShop.Library.Exceptions;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;

namespace OnlineShop.ProductManagementService.Middleware
{
    public class CustomExceptionHandlerMiddleware
    {
        readonly RequestDelegate _next;

        public CustomExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                await HandleExceptionAsync(context, exception);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError;
            var jsonResponse = JsonSerializer.Serialize(new { message = exception.Message });

            switch (exception)
            {
                case ValidationException:
                    code = HttpStatusCode.BadRequest;
                    break;
                case NotFoundException:
                    code = HttpStatusCode.NotFound;          
                    break;
            }
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            return context.Response.WriteAsync(jsonResponse);
        }
    }
}
