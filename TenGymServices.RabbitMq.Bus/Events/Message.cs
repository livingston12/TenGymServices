using MediatR;

namespace TenGymServices.RabbitMq.Bus.Events
{
    public abstract class Message : IRequest<bool>
    {
        public string MessageType { get; protected set; }

        protected Message(){
            MessageType = GetType().Name;
        }
    }
}