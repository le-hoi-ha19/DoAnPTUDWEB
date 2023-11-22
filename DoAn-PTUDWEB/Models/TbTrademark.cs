using System;
using System.Collections.Generic;

namespace DoAn_PTUDWEB.Models
{
    public partial class TbTrademark
    {
        public TbTrademark()
        {
            TbProducts = new HashSet<TbProduct>();
        }

        public int TrademarkId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string Logo { get; set; } = null!;

        public virtual ICollection<TbProduct> TbProducts { get; set; }
    }
}
