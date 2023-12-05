using System;
using System.Collections.Generic;

namespace DoAn_PTUDWEB.Models
{
    public partial class TbOrderDetail
    {
        public int? OrderId { get; set; }
        public int? ProductId { get; set; }
        public int? Quantity { get; set; }
        public int? TotalMoney { get; set; }
        public decimal? Price { get; set; }

        public virtual TbOrder? Order { get; set; }
        public virtual TbProduct? Product { get; set; }
    }
}
