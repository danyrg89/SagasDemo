using Orders.SagasData;
using Rebus.Sagas;

namespace Orders.Sagas
{

    public class CreateOrderSaga : Saga<CreateOrderSagaData>
    {
        protected override void CorrelateMessages(ICorrelationConfig<CreateOrderSagaData> config)
        {
            throw new NotImplementedException();
        }
    }
}
