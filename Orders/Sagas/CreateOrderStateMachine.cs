using EventBus.Consts;
using EventBus.Events.OrderEvents;
using EventBus.EventsProductEvents;
using EventBus.Messages.NotificationMessages;
using EventBus.Messages.OrderMessages;
using EventBus.Messages.PaymentMessages;
using MassTransit;
using Orders.StateMachineInstances;

namespace Orders.Sagas
{

    public class CreateOrderStateMachine : MassTransitStateMachine<CreateOrderStateMachineInstance>
    {
        // Commands
        public Event<CreateOrderMessage> CreateOrderMessage { get; set; }

        public Event<ReserveProductsMessage> ReserveProductsMessage { get; set; }

        public Event<OrderCompletedMessage> OrderCompletedMessage { get; set; }

        // Events
        public Event<StartOrderCreationEvent> StartOrderCreationEvent { get; set; }

        public Event<OrderInitiatedEvent> OrdeInitiatedEvent { get; set; }

        public Event<OrderInitiFailEvent> OrderCreationFailEvent { get; set; }

        public Event<ProductsReservedEvent> ProductsReservedEvent { get; set; }

        public Event<ProductsReservationFailEvent> ProductsReservationFailEvent { get; set; }

        public Event<OrderFinalizedEvent> OrderFinalizedEvent { get; set; }

        public Event<OrderFinalizedFailureEvent> OrderFinalizedFailureEvent { get; set; }

        // States
        private State PlacingOrder { get; set; }

        private State OrderCreated { get; set; }

        private State ProductsReserved { get; set; }


        public CreateOrderStateMachine()
        {
            InstanceState(x => x.CurrentState);

            State(() => PlacingOrder);
            State(() => ProductsReserved);
            State(() => OrderCreated);

            Initially(
                When(StartOrderCreationEvent)
                    .Then(context => {
                        context.Saga.Order = context.Message.Order;
                        context.Saga.CorrelationId = context.Message.CorrelationId;
                        Console.WriteLine("Message received!!");

                    })
                    .Send(new Uri($"queue:{QueueConst.CreateOrderQueue}"),
                            context => new CreateOrderMessage(context.Message.CorrelationId, context.Message.Order))
                    .TransitionTo(PlacingOrder));

            During(PlacingOrder,
                When(OrdeInitiatedEvent)
                    .Then(context =>
                    {
                        Console.WriteLine("Order Initiated now is time to Reserve the Products!");
                    })
                    .Send(new Uri($"queue:{QueueConst.ReserveProductsQueue}"),
                        context => new ReserveProductsMessage(context.Message.CorrelationId, context.Message.Order))
                    .TransitionTo(ProductsReserved));

            During(ProductsReserved,
                When(ProductsReservedEvent)
                    .Then(context => { 
                        Console.WriteLine("Products Reserved now is time to Complete the Order!");
                    })
                    .Send(new Uri($"queue:{QueueConst.OrderCompletedQueue}"),
                        context => new OrderCompletedMessage(context.Message.CorrelationId, context.Message.Order))
                    .TransitionTo(OrderCreated)
                );

            During(OrderCreated,
                When(OrderFinalizedEvent)
                    .Then(context =>
                    {
                        Console.WriteLine("Order Finalized!");
                    })
                    .Finalize(),
                When(OrderFinalizedFailureEvent)
                    
                );
        }
    }
}
