using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.EventsProductEvents
{
    public record ProductsReservedEvent(Guid CorrelationId, Guid OrderId) : CorrelatedBy<Guid>;

    public record ProductsReservationFailEvent(Guid CorrelationId, Guid OrderId) : CorrelatedBy<Guid>;
}
