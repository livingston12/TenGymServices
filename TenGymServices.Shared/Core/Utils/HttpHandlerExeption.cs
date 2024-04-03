namespace TenGymServices.Shared.Core.Utils
{
    public class HttpHandlerExeption : Exception
    {
        public HttpHandlerExeption() { }
        public HttpHandlerExeption(string message) : base(message) { }

        public HttpHandlerExeption(string message, Exception innerExeption)
            : base(message, innerExeption) { }
    }
}