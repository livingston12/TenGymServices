
using System.Net;
using Newtonsoft.Json;
using TenGymServices.Shared.Core.Extentions;
using TenGymServices.Shared.Core.Utils;

namespace TenGymServices.Api.Plans.Core.Utils
{
    public class ExceptionHandlerMiddleware : IMiddleware
    {
        private readonly IWebHostEnvironment _env;
        public ExceptionHandlerMiddleware(IWebHostEnvironment env)
        {
            _env = env;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (ValidatorExeption ex)
            {
                var response = new { ErrorMessage = ex.Message.Split("\n").Where(x => x.Length > 0) };
                await WriteAsync(context, HttpStatusCode.BadRequest, response);
            }
            catch (HttpHandlerExeption ex)
            {
                var response = new { ErrorMessage = ex.Message };
                var messageParser = ex.Message.Split('+');
                if (messageParser.Length > 1)
                {
                    var errorMessage = messageParser[0];
                    int.TryParse(messageParser[1], out int StatusCode );
                    var httpCode = StatusCode.GetEnumStatusCode();
                    response = new { ErrorMessage = errorMessage };
                    
                    await WriteAsync(context, httpCode, response);
                    return;
                }

                await WriteAsync(context, HttpStatusCode.InternalServerError, response);
            }
            catch (Exception ex)
            {
                var response = new { ErrorMessage = "Internal server error please contact support" };
                if (!_env.IsDevelopment()) 
                {
                    response = new { ErrorMessage = ex.Message };
                }
                
                await WriteAsync(context, HttpStatusCode.InternalServerError, response);
            }
        }

        private async Task WriteAsync(HttpContext context, HttpStatusCode statusCodes, object response)
        {
            context.Response.StatusCode = (int)statusCodes;
            context.Response.ContentType = "application/json";
            var jsonResponse = JsonConvert.SerializeObject(response);
            await context.Response.WriteAsync(jsonResponse);
        }
    }
}