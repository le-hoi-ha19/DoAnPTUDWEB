using System;
using System.Collections.Generic;

namespace DoAn_PTUDWEB.Models
{
    public partial class TbOrder
    {
        public int UserId { get; set; }
        public int OrderId { get; set; }
        public DateTime? OrderDate { get; set; }
        public string? ShipAddress { get; set; }
        public string? Note { get; set; }
        public string? Status { get; set; }

        public virtual TbUser User { get; set; } = null!;
    }
}
