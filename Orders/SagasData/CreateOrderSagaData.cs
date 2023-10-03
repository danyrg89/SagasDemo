using Rebus.Sagas;

namespace Orders.SagasData
{
    public class CreateOrderSagaData : ISagaData
    {
        public Guid Id { get; set; }
        public int Revision { get; set; }
        public Guid OrderId { get; set; }
        public bool OrderCreated { get; set; }
        public bool InventoryUpdated { get; set; }
    }

}
