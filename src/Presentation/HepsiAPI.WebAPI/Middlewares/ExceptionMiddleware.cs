using FluentValidation;
using HepsiAPI.Application.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HepsiAPI.WebAPI.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }


        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            var response = GetErrorDetail(exception);
            context.Response.StatusCode = response.Status.Value;

            return context.Response.WriteAsync(JsonConvert.SerializeObject(response));
        }

        private ProblemDetails GetErrorDetail(Exception exception)
        {
            return exception switch
            {
                ValidationException => CreateValidationException(exception),
                AuthorizationException => CreateAuthorizationException(exception),
                BusinessException => CreateBusinessException(exception),
                _ => CreateInternalException(exception),
            };


        }

        private ValidationErrorDetails CreateValidationException(Exception exception)
        {
            return new ValidationErrorDetails()
            {

                Status = StatusCodes.Status400BadRequest,
                Type = "https://example.com/probs/validation",
                Title = "Validation error(s)",
                Detail = exception.Message,
                Instance = "",
                Errors = ((ValidationException)exception).Errors,
            };
        }

        private AuthorizationErrorDetails CreateAuthorizationException(Exception exception)
        {
            return new AuthorizationErrorDetails()
            {
                Status = StatusCodes.Status401Unauthorized,
                Type = "https://example.com/probs/authorization",
                Title = "Authorization exception",
                Detail = exception.Message,
                Instance = ""
            };
        }

        private BusinessErrorDetails CreateBusinessException(Exception exception)
        {

            return new BusinessErrorDetails()
            {
                Status = StatusCodes.Status400BadRequest,
                Type = "https://example.com/probs/business",
                Title = "Business exception",
                Detail = exception.Message,
                Instance = ""

            };

        }

        private ProblemDetails CreateInternalException(Exception exception)
        {
            return new ProblemDetails()
            {
                Status = StatusCodes.Status500InternalServerError,
                Type = "https://example.com/probs/internal",
                Title = "Internal exception",
                Detail = exception.Message,
                Instance = ""
            };


        }

    }


}
