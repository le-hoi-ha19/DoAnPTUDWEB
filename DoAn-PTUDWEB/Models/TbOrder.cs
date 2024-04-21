using System;
using System.Collections.Generic;

namespace DoAn_PTUDWEB.Models
{
    public partial class TbOrder
    {
        public TbOrder()
        {
            TbOrderDetails = new HashSet<TbOrderDetail>();
        }

        public int UserId { get; set; }
        public int OrderId { get; set; }
        public DateTime? OrderDate { get; set; }
        public string? ShipAddress { get; set; }
        public string? Note { get; set; }
        public int? Status { get; set; }

        public virtual TbUser User { get; set; } = null!;
        public virtual ICollection<TbOrderDetail> TbOrderDetails { get; set; }
    }
}
