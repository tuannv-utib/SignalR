using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using SignalR.Models;
using System.Threading.Tasks;

namespace SignalR.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        public async Task BroadcastChatData(MessageDetails data) => await Clients.All.SendAsync("new_mess", data);

        public async Task SendAll()
        {
            var data = Context;
            await Clients.All.SendAsync("new_mess", "ok?");
            await Clients.User("100").SendAsync("new_mess", "send to user_id = 100");
        }
    }
}
