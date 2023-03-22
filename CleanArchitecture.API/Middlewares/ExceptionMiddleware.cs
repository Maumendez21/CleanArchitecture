using CleanArchitecture.API.Errors;
using CleanArchitecture.Application.Exceptions;
using Newtonsoft.Json;
using System.Net;

namespace CleanArchitecture.API.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _environment;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment environment)
        {
            _next = next;
            _logger = logger;
            _environment = environment;
        }


        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                context.Response.ContentType = "application/json";
                int statusCode = (int)HttpStatusCode.InternalServerError;
                string result = string.Empty;


                switch (e)
                {
                    case DirectoryNotFoundException notFoundException:
                        statusCode = (int)HttpStatusCode.NotFound;
                        break;
                    case ValidationException validationException:
                        statusCode = (int)HttpStatusCode.BadRequest;
                        string validationJson = JsonConvert.SerializeObject(validationException.Errors);
                        result = JsonConvert.SerializeObject(new CodeErrorException(statusCode, e.Message, validationJson));
                        break;
                    case BadRequestException badRequestException:
                        statusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    default:
                        break;
                }

                if (string.IsNullOrEmpty(result)) result = JsonConvert.SerializeObject(new CodeErrorException(statusCode, e.Message, e.StackTrace));
                
                context.Response.StatusCode = statusCode;

                /*OTRA FORMA SIN TIPADO DE EXCEPCIONES*/
                //CodeErrorException response = _environment.IsDevelopment()
                //    ? new CodeErrorException((int)HttpStatusCode.InternalServerError, e.Message, e.StackTrace) 
                //    : new CodeErrorException((int)HttpStatusCode.InternalServerError);

                //JsonSerializerOptions options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

                //string json = JsonSerializer.Serialize(response, options);

                await context.Response.WriteAsync(result);
            }
        }
    }
}
