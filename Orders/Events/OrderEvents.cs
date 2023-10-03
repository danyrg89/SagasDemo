namespace Orders.Events
{
    public class OrderEvents
    {
        public record OrderCreatedEvent(Guid orderId);

        public record InventoryUpdatedEvent(Guid orderId);

    }
}
