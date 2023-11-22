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
            TbTuKhoaSanPhams = new HashSet<TbTuKhoaSanPham>();
        }

        public int ProductId { get; set; }
        public int CategoryProductId { get; set; }
        public string Name { get; set; } = null!;
        public bool IsBestSeller { get; set; }
        public bool IsHot { get; set; }
        public bool IsNew { get; set; }
        public int? Price { get; set; }
        public int? PriceSale { get; set; }
        public int? Quantity { get; set; }
        public string? Thumbnail { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public int? TrademarkId { get; set; }
        public bool? Deleted { get; set; }
        public string? Description { get; set; }
        public bool? IsActive { get; set; }

        public virtual TbProductCategory CategoryProduct { get; set; } = null!;
        public virtual TbTrademark? Trademark { get; set; }
        public virtual TbReview? TbReview { get; set; }
        public virtual ICollection<TbImageProduct> TbImageProducts { get; set; }
        public virtual ICollection<TbOrderDetail> TbOrderDetails { get; set; }
        public virtual ICollection<TbTuKhoaSanPham> TbTuKhoaSanPhams { get; set; }
    }
}
