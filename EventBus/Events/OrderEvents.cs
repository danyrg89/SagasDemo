using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Events.OrderEvents
{

    public record OrderCreatedEvent(Guid CorrelationId, Guid OrderId) : CorrelatedBy<Guid>;

    public record OrderCreationFailEvent(Guid CorrelationId, Guid OrderId) : CorrelatedBy<Guid>;

}
