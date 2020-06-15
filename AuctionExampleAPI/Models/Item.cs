using System;
using System.Collections.Generic;

namespace AuctionExampleAPI.Models
{
    public partial class Item
    {
        public Item()
        {
            Rate = new HashSet<Rate>();
        }

        public int ItemId { get; set; }
        public string Name { get; set; }
        public int StartPrice { get; set; }
        public int CurrentPrice { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool Status { get; set; }

        public virtual ICollection<Rate> Rate { get; set; }
    }
}
