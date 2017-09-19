using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using WEB.ViewModels;

namespace WEB.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate requestDelegate, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = requestDelegate;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception exception)
            {
                await HandleExceptionAsync(context, exception);
            }

            if (!context.Response.HasStarted)
            {
                context.Response.ContentType = "application/json";
                String json;
                if (context.Response.StatusCode == StatusCodes.Status401Unauthorized)
                {

                    json = SerializeToJson(StatusCodes.Status401Unauthorized, "Bearer error=invalid_token");
                }
                else
                {
                    json = SerializeToJson(StatusCodes.Status500InternalServerError, "Internal server error");
                }

                _logger.LogError(json);

                await context.Response.WriteAsync(json);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError; 

            var result = SerializeToJson(StatusCodes.Status500InternalServerError, exception.Message);

            _logger.LogError(exception.ToString());
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int) code;
            return context.Response.WriteAsync(result);
        }

        private String SerializeToJson(Int32 statusCode, String message)
        {
            return JsonConvert.SerializeObject(
                new ApiResponse(statusCode, null, message), new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                });
        }
    }

    public static class ErrorHanlingMiddelewareException
    {
        public static IApplicationBuilder UseErrorHandling(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ErrorHandlingMiddleware>();
        }
    }
}