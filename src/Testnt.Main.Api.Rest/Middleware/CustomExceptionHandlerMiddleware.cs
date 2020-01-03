using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using Testnt.Common.Exceptions;

// reference : https://lurumad.github.io/problem-details-an-standard-way-for-specifying-errors-in-http-api-responses-asp.net-core
namespace Testnt.Main.Api.Rest.Middleware
{
    public class CustomExceptionHandlerMiddleware
    {
        private readonly RequestDelegate next;
        private readonly IHostEnvironment host;

        public CustomExceptionHandlerMiddleware(RequestDelegate next, IHostEnvironment host)
        {
            this.next = next;
            this.host = host;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                var body = context.Response.StatusCode;
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var problemDetail = new ProblemDetails()
            {
                Title = exception.Message,
                Detail = exception.StackTrace,
                Instance = context.Request.Path,
                Status = StatusCodes.Status500InternalServerError,

            };

            // for security reason, not leaking any implementation/error detail in production
            if (host.IsProduction())
            {
                problemDetail.Detail = "Unexpected error occured";
            }

            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "applications/problem+json";

            switch (exception)
            {
                case ApplicationValidationException applicationValidationException:
                    var validationProblem = new ValidationProblemDetails(applicationValidationException.Failures)
                    {
                        Title = problemDetail.Title,
                        Detail = problemDetail.Detail,
                        Instance = problemDetail.Instance
                    };
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    validationProblem.Status = StatusCodes.Status400BadRequest;
                    return context.Response.WriteAsync(JsonConvert.SerializeObject(validationProblem));
                case ApplicationBadRequestException _:
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    problemDetail.Status = StatusCodes.Status400BadRequest;
                    break;
                case EntityDeleteFailureException _:
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    problemDetail.Status = StatusCodes.Status400BadRequest;
                    break;
                case EntityNotFoundException _:
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                    problemDetail.Status = StatusCodes.Status404NotFound;
                    break;
            }



            return context.Response.WriteAsync(JsonConvert.SerializeObject(problemDetail));
        }
    }

    public static class CustomExceptionHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionHandlerMiddleware>();
        }
    }
}
