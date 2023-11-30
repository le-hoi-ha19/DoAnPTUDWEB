using System;
using System.Collections.Generic;

namespace DoAn_PTUDWEB.Models
{
    public partial class TbProductColor
    {
        public int ProductColorId { get; set; }
        public int ColorId { get; set; }
        public int ProductId { get; set; }
        public string? Description { get; set; }

        public virtual TbColor Color { get; set; } = null!;
        public virtual TbProduct Product { get; set; } = null!;
    }
}
