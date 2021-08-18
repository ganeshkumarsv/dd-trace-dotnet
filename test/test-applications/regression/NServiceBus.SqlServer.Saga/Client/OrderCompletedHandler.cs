using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Logging;
using NServiceBus.SqlServer.Saga.Shared;

namespace NServiceBus.SqlServer.Saga.Client
{
    public class OrderCompletedHandler : IHandleMessages<OrderCompleted>
    {
        static readonly ILog log = LogManager.GetLogger<OrderCompletedHandler>();
        static Task CompletedTask = Task.FromResult(0);

        public Task Handle(OrderCompleted message, IMessageHandlerContext context)
        {
            log.Info($"Received OrderCompleted for OrderId {message.OrderId}");
            Program.Countdown.Signal(); // Send CountdownEvent signal so the program knows when to exit
            return CompletedTask;
        }
    }
}
