using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoAn_PTUDWEB.Models
{
    public partial class TbProduct
    {
        public TbProduct()
        {
            TbImageProducts = new HashSet<TbImageProduct>();
            TbProductColors = new HashSet<TbProductColor>();
            TbTuKhoaSanPhams = new HashSet<TbTuKhoaSanPham>();
        }

        public int ProductId { get; set; }
        [ForeignKey("TbCategory")]
        public int CategoryProductId { get; set; }
        public virtual TbProductCategory CategoryProduct { get; set; } = null!;
        public string Name { get; set; } = null!;
        public bool IsBestSeller { get; set; }
        public bool IsHot { get; set; }
        public bool IsNew { get; set; }
        public int? Price { get; set; }
        public decimal? PriceDiscount { get; set; }
        public int? Quantity { get; set; }
        public string? Thumbnail { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? ModifiedBy { get; set; }
        [ForeignKey("TbTrademark")]
        public int? TrademarkId { get; set; }
        public virtual TbTrademark Trademark { get; set; }
        public bool? Deleted { get; set; }
        public string? Description { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<TbImageProduct> TbImageProducts { get; set; }
        public virtual ICollection<TbProductColor> TbProductColors { get; set; }
        public virtual ICollection<TbTuKhoaSanPham> TbTuKhoaSanPhams { get; set; }
    }
}
