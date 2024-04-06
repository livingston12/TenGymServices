namespace TenGymServices.Shared.Core.Interfaces
{

    public interface IPaypalService<in TRequest>
        where TRequest : class
    {
        public Task<(bool hasEerror, string Id, string MessageError)> PostAsync(TRequest paypalRequest, string method);
    }
}

