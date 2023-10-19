using EventBus.Entities;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Messages.NotificationMessages
{
    public record NotifyOrderCreatedMessage(Guid CorrelationId, Order Order) : CorrelatedBy<Guid>;

    public record NotifyPaymentProcessedMessage(Guid CorrelationId, Order Order) : CorrelatedBy<Guid>;

    public record NotifyOrderREadyToShipMessage(Guid CorrelationId, Order Order) : CorrelatedBy<Guid>;


}
