using Microsoft.AspNetCore.SignalR;

namespace HotelManagementSystem.WebApp.Services
{
    public class ChatHubService : Hub
    {
        public async Task SendReservation(string Name)
        {
            try
            {
                await Clients.All.SendAsync("ReservationReceive", Name);
            }
            catch (Exception ex)
            {
                await Clients.Caller.SendAsync("ReceiveErrorMessage", "Server Error: " + ex.Message);
            }
        }
    }
}
