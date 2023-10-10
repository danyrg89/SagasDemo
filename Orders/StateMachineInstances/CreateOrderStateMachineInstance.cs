using MassTransit;

namespace Orders.StateMachineInstances
{
    public class CreateOrderStateMachineInstance : SagaStateMachineInstance
    {
        public Guid CorrelationId
        {
            get
            {
                return OrderId;
            }
            set
            {
                OrderId = value;
            }
        }

        public Guid OrderId { get; set; }

        public string CurrentState { get; set; }
    }

}
