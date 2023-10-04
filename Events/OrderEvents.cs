namespace Events.OrderEvents
{
    public record OrderCreatedEvent(Guid OrderId);

    public record ReserveProductsEvent(Guid OrderId);

    public record InventoryUpdatedEvent(Guid OrderId);

}
