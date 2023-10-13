using EventBus.Consts;
using EventBus.Events.OrderEvents;
using EventBus.Messages.NotificationMessages;
using EventBus.Messages.OrderMessages;
using MassTransit;
using Orders.StateMachineInstances;

namespace Orders.Sagas
{

    public class CreateOrderStateMachine : MassTransitStateMachine<CreateOrderStateMachineInstance>
    {

        // Commands
        public Event<StartOrderCreationSagaMessage> StartOrderCreationSagaMessage { get; set; }

        public Event<CreateOrderMessage> CreateOrderMessage { get; set; }

        public Event<NotifyOrderCreatedMessage> NotifyOrderCreatedMessage { get; set; }

        // Events
        public Event<OrderCreatedEvent> OrderCreatedEvent { get; set; }

        public Event<OrderCreationFailEvent> OrderCreationFailEvent { get; set; }

        // States
        private State PlacingOrder { get; set; }

        private State OrderCreated { get; set; }

        //private State PaymentProcessed { get; set; }


        public CreateOrderStateMachine()
        {
            InstanceState(x => x.CurrentState);

            State(() => PlacingOrder);
            State(() => OrderCreated);

            Initially(
                When(StartOrderCreationSagaMessage)
                    .Then(context => {
                        context.Saga.OrderId = context.Message.OrderId;
                        context.Saga.CorrelationId = context.Message.CorrelationId;
                        Console.WriteLine("Message received!!");

                    })
                    .Send(new Uri($"queue:{QueueConst.CreateOrderQueueName}"),
                            context => new CreateOrderMessage(context.Message.CorrelationId, context.Message.OrderId))
                    .TransitionTo(PlacingOrder));

            During(PlacingOrder,
                When(OrderCreatedEvent)
                    .Then(context => {
                        Console.WriteLine("Order Created Event!");
                    })
                    .TransitionTo(OrderCreated));

        }
    }
}
