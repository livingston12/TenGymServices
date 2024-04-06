using System.Text.Json.Serialization;

namespace TenGymServices.Shared.Core.Interfaces;

public interface IId
{
    [JsonPropertyName("id")]
    public string Id { get; set; }
}
