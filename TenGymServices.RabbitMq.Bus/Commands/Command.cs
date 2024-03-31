using TenGymServices.RabbitMq.Bus.Events;

namespace TenGymServices.RabbitMq.Bus.Commands
{
    public abstract class Command : Message
    {
        public DateTime TimeStamp { get; protected set; }

        protected Command() 
        {
            TimeStamp = DateTime.Now;
        }
    }
}