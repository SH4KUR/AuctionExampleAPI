using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace AuctionExampleAPI.Hubs
{
    public class RefreshHub : Hub<IReferenceHub>
    {
        public async Task Refresh(int idItem)
        {
            await Clients.All.RefreshItem(idItem);
        }
    }
}
