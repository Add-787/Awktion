using Microsoft.AspNet.SignalR.Hubs;

namespace Awktion.API.Middleware;
public class HubLogging : HubPipelineModule
{
    protected override bool OnBeforeIncoming(IHubIncomingInvokerContext context)
    {
        return base.OnBeforeIncoming(context);
    }

    protected override bool OnBeforeOutgoing(IHubOutgoingInvokerContext context)
    {
        return base.OnBeforeOutgoing(context);
    }

    protected override void OnIncomingError(ExceptionContext exceptionContext, IHubIncomingInvokerContext invokerContext)
    {
        base.OnIncomingError(exceptionContext, invokerContext);
    }

}