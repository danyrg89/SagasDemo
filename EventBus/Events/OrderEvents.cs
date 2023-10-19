using EventBus.Entities;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Events.OrderEvents
{

    public record OrderInitiatedEvent(Guid CorrelationId, Order Order) : CorrelatedBy<Guid>;

    public record OrderInitiFailEvent(Guid CorrelationId, Order Order) : CorrelatedBy<Guid>;

}
