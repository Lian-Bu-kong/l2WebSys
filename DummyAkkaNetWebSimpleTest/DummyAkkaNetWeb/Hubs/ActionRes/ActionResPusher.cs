using AkkaSys.Event.ActionRes;
using Microsoft.AspNetCore.SignalR;

namespace DummyAkkaNetWeb.Hubs.ActionRes
{
    public class ActionResPusher : IActionResPusher
    {
        private readonly IHubContext<ActionResRequest> _actionResContext;

        public ActionResPusher(IHubContext<ActionResRequest> actionResContext)
        {
            _actionResContext = actionResContext;
        }

        public void AlterMsg(string msg)
        {
            _actionResContext.Clients.All.SendAsync("AlterMsg", msg);
        }
    }
}
