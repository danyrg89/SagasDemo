//using Events.OrderEvents;
//using Rebus.Bus;
//using Rebus.Handlers;

//namespace Products.Handlers
//{
//    public class InventoryHandler : IHandleMessages<ReserveProductsEvent>
//    {
//        public IBus Bus { get; }
        
//        public InventoryHandler(IBus bus)
//        {
//            Bus = bus;
//        }

//        public async Task Handle(ReserveProductsEvent message)
//        {
//            Console.WriteLine("Handling Inventory Update Event...");
            
//            await Bus.Send(new InventoryUpdatedEvent(message.OrderId));

//            Console.WriteLine("Inventory Updated ...");
//        }
//    }
//}
