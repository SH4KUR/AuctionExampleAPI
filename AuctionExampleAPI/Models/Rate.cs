using System;

namespace AuctionExampleAPI.Models
{
    public partial class Rate
    {
        public int RateId { get; set; }
        public int ItemId { get; set; }
        public string UserName { get; set; }
        public DateTime RateTime { get; set; }
        public int Price { get; set; }

        public virtual Item Item { get; set; }
    }
}
