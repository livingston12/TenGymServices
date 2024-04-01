namespace TenGymServices.RabbitMq.Bus.BusRabbit;

public interface IConfigurationRabbitmq
{
    public string HostName { get; set; }
    public string Exchange { get; set; }
}
