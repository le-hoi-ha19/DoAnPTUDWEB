using System;
using System.Collections.Generic;

namespace DoAn_PTUDWEB.Models
{
    public partial class TbImageProduct
    {
        public int ImageProductId { get; set; }
        public string Thumbnail { get; set; } = null!;
        public int ProductId { get; set; }

        public virtual TbProduct Product { get; set; } = null!;
    }
}
