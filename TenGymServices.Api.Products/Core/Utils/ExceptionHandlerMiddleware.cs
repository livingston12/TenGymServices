
using System.Net;
using Newtonsoft.Json;
using TenGymServices.Shared.Core.Utils;

namespace TenGymServices.Api.Products.Core.Utils
{
    public class ExceptionHandlerMiddleware : IMiddleware
    {
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
            catch (Exception ex)
            {
                var response = new { ErrorMessage = ex.Message };
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