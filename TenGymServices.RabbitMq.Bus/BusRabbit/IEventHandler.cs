using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TenGymServices.RabbitMq.Bus.Events;

namespace TenGymServices.RabbitMq.Bus.BusRabbit
{
    public interface IEventHandler<in TEvent> : IEventHandler where TEvent : Event
    {
        Task Handle(TEvent @event);
    }

    public interface IEventHandler 
    {
        
    }
}