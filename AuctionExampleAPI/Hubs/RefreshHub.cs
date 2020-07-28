using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace AuctionExampleAPI.Hubs
{
    public class RefreshHub : Hub
    {
        public void Refresh(int idItem)
        {
            Clients.All.SendAsync("refresh", idItem);
        }
    }
}
