using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace DummyAkkaNetWeb.Hubs
{
    /// <summary>
    /// Author : ICSC 士鵬
    /// Desc : ChatHub (Actor與前端UI溝通)
    /// </summary>
    public class ChatHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}
