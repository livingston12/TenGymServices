using System.Net;

namespace TenGymServices.Shared.Core.Extentions
{
    public static class IntExtention
    {
        public static HttpStatusCode GetEnumStatusCode(this int statusCodeNumber) 
        {
            return (HttpStatusCode)Enum.Parse(typeof(HttpStatusCode), statusCodeNumber.ToString());
        }
        
    }
}