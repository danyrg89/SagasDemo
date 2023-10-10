using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Messages.OrderMessages
{
    public record StartOrderCreationSagaMessage(Guid CorrelationId, Guid OrderId) : CorrelatedBy<Guid>;
    public record CreateOrderMessage(Guid CorrelationId, Guid OrderId) : CorrelatedBy<Guid>;

}
