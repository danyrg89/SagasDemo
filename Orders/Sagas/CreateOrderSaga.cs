using Orders.SagasData;
using Rebus.Handlers;
using Rebus.Sagas;
using Events.OrderEvents;
using Rebus.Bus;

namespace Orders.Sagas
{

    public class CreateOrderSaga : Saga<CreateOrderSagaData>,
        IAmInitiatedBy<OrderCreatedEvent>,
        IHandleMessages<InventoryUpdatedEvent>
    {
        public IBus Bus { get; }

        public CreateOrderSaga(IBus bus)
        {
            Bus = bus;
        }


        protected override void CorrelateMessages(ICorrelationConfig<CreateOrderSagaData> config)
        {
            config.Correlate<OrderCreatedEvent>(m => m.OrderId, s => s.OrderId);
            config.Correlate<InventoryUpdatedEvent>(m => m.OrderId, s => s.OrderId);
            config.Correlate<InventoryUpdatedEvent>(m => m.OrderId, s => s.OrderId);
        }

        public async Task Handle(OrderCreatedEvent message)
        {
            Bus.Send(new ReserveProductsEvent(message.OrderId));
        }

        public async Task Handle(InventoryUpdatedEvent message)
        {
            Console.WriteLine("Inventory Updated...");
            
            this.MarkAsComplete();

            Console.WriteLine("End of Saga");
        }
    }
}
