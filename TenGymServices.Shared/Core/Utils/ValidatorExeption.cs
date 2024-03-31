namespace TenGymServices.Shared.Core.Utils
{
    public class ValidatorExeption : Exception
    {
        public ValidatorExeption() { }
        public ValidatorExeption(string message) : base(message) { }

        public ValidatorExeption(string message, Exception innerExeption)
            : base (message, innerExeption) { }
    }
}