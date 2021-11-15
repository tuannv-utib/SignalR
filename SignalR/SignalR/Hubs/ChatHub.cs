using Microsoft.AspNetCore.SignalR;
using SignalR.Models;
using System.Threading.Tasks;

namespace SignalR.Hubs
{
    public class ChatHub: Hub
    {
        public async Task BroadcastChatData(MessageDetails data) => await Clients.All.SendAsync("new_mess", data);
    }
}
