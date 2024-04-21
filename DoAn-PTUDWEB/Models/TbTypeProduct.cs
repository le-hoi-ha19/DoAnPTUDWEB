using System;
using System.Collections.Generic;

namespace DoAn_PTUDWEB.Models
{
    public partial class TbTypeProduct
    {
        public int? TypeId { get; set; }
        public int? ProductId { get; set; }
        public int TypeProductId { get; set; }

        public virtual TbProduct? Product { get; set; }
        public virtual TbType? Type { get; set; }
    }
}
