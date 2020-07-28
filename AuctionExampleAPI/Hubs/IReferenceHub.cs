 using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionExampleAPI.Hubs
{
    public interface IReferenceHub
    {
        Task RefreshItem(int idItem);
    }
}
