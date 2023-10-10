using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Events.PaymentEvents
{
    public record PaymentProcessedEvent(Guid CorrelationId, Guid OrderId) : CorrelatedBy<Guid>;

    public record PaymentProcessingFailEvent(Guid CorrelationId, Guid OrderId) : CorrelatedBy<Guid>;
}
