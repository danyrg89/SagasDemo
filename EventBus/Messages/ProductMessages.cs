using EventBus.Entities;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Messages.PaymentMessages
{
    public record ReserveProductsMessage(Guid CorrelationId, Order Order) : CorrelatedBy<Guid>;
}
