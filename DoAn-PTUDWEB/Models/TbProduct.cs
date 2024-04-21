using System;
using System.Collections.Generic;

namespace DoAn_PTUDWEB.Models
{
    public partial class TbProduct
    {
        public TbProduct()
        {
            TbImageProducts = new HashSet<TbImageProduct>();
            TbOrderDetails = new HashSet<TbOrderDetail>();
            TbProductColors = new HashSet<TbProductColor>();
            TbTypeProducts = new HashSet<TbTypeProduct>();
        }

        public int ProductId { get; set; }
        public int CategoryProductId { get; set; }
        public string Name { get; set; } = null!;
        public bool IsBestSeller { get; set; }
        public bool IsHot { get; set; }
        public bool IsNew { get; set; }
        public int? Price { get; set; }
        public decimal? PriceDiscount { get; set; }
        public int? Quantity { get; set; }
        public string? Thumbnail { get; set; }
        public int TrademarkId { get; set; }
        public string? Description { get; set; }
        public bool? IsActive { get; set; }

        public virtual TbProductCategory CategoryProduct { get; set; } = null!;
        public virtual TbTrademark Trademark { get; set; } = null!;
        public virtual ICollection<TbImageProduct> TbImageProducts { get; set; }
        public virtual ICollection<TbOrderDetail> TbOrderDetails { get; set; }
        public virtual ICollection<TbProductColor> TbProductColors { get; set; }
        public virtual ICollection<TbTypeProduct> TbTypeProducts { get; set; }
    }
}
