using Microsoft.AspNetCore.SignalR;

namespace DemoSignalRIA_Client.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string message)
        {
            await Clients.All.SendAsync("receiveMessage", message);
        }

        public async Task JoinGroup(string groupname = "mongroup")
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupname);
            await Clients.Group(groupname).SendAsync("messagetogroup", Context.ConnectionId + " Has Joined");
        }

        public override async Task OnConnectedAsync()
        {
            await Clients.All.SendAsync("receiveMessage", Context.ConnectionId + " Has Joined");
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            await Clients.All.SendAsync("receiveMessage", Context.ConnectionId + " s'est tiré");

        }
    }
}
