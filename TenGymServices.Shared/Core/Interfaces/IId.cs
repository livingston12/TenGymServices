using Newtonsoft.Json;

namespace TenGymServices.Shared.Core.Interfaces;

public interface IId
{
    [JsonProperty("id")]
    public string Id { get; set; }
}
