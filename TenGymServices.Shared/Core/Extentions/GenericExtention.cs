using System.Net;
using TenGymServices.Shared.Core.Utils;

namespace TenGymServices.Shared.Core.Extentions
{
    public static class GenericExtention
    {
        public static void ThrowHttpHandlerExeption<T>(this T obj, string errorMessage, HttpStatusCode statusCode)
            where T : class 
        {
            throw new HttpHandlerExeption($"{errorMessage}+{(int)statusCode}");
        }
    }
}