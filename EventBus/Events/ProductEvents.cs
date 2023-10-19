using EventBus.Entities;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.EventsProductEvents
{
    public record ProductsReservedEvent(Guid CorrelationId, Order Order) : CorrelatedBy<Guid>;

    public record ProductsReservationFailEvent(Guid CorrelationId, Order Order) : CorrelatedBy<Guid>;
}
