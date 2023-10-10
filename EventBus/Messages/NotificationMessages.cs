using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Messages.NotificationMessages
{
    public record NotifyOrderCreatedMessage(Guid CorrelationId, Guid OrderId) : CorrelatedBy<Guid>;

    public record NotifyPaymentProcessedMessage(Guid CorrelationId, Guid OrderId) : CorrelatedBy<Guid>;

    public record NotifyOrderREadyToShipMessage(Guid CorrelationId, Guid OrderId) : CorrelatedBy<Guid>;


}
