using System;
using System.Collections.Generic;

namespace DoAn_PTUDWEB.Models
{
    public partial class TbColor
    {
        public TbColor()
        {
            TbOrderDetails = new HashSet<TbOrderDetail>();
            TbProductColors = new HashSet<TbProductColor>();
        }

        public int ColorId { get; set; }
        public string? ColorName { get; set; }
        public string? Description { get; set; }

        public virtual ICollection<TbOrderDetail> TbOrderDetails { get; set; }
        public virtual ICollection<TbProductColor> TbProductColors { get; set; }
    }
}
