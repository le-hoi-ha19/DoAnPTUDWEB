using System;
using System.Collections.Generic;

namespace DoAn_PTUDWEB.Models
{
    public partial class TbOrderDetail
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int? ColorId { get; set; }
        public int? TypeId { get; set; }
        public int? Quantity { get; set; }
        public decimal? TotalMoney { get; set; }
        public decimal? Price { get; set; }

        public virtual TbColor? Color { get; set; }
        public virtual TbOrder Order { get; set; } = null!;
        public virtual TbProduct Product { get; set; } = null!;
        public virtual TbType? Type { get; set; }
    }
}
