using log4net;
using Messages;
using NServiceBus;

public class RequestMessageHandler : IHandleMessages<Request>
{
    static ILog log = LogManager.GetLogger(typeof(RequestMessageHandler));
    IBus bus;

    public RequestMessageHandler(IBus bus)
    {
        this.bus = bus;
    }

    public void Handle(Request message)
    {
        log.Info("Request received with id:" + message.RequestId);

        bus.Reply(new Response
        {
            ResponseId = message.RequestId
        });
    }
}