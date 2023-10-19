using EventBus.Entities;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Messages.OrderMessages
{
    public record StartOrderCreationEvent(Guid CorrelationId, Order Order) : CorrelatedBy<Guid>;
    public record CreateOrderMessage(Guid CorrelationId, Order Order) : CorrelatedBy<Guid>;
    public record OrderCompletedMessage(Guid CorrelationId, Order Order) : CorrelatedBy<Guid>;
    public record OrderFinalizedEvent(Guid CorrelationId, Order Order) : CorrelatedBy<Guid>;
    public record OrderFinalizedFailureEvent(Guid CorrelationId, Order Order) : CorrelatedBy<Guid>;
    

}
