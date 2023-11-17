using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;

namespace DoAn_PTUDWEB.Models
{
    [Table("tb_Product")] // Ánh xạ với bảng Product trong cơ sở dữ liệu
    public class Product
    {
        [Key]
        public int ProductId {  get; set; } 

        public int CategoryProductId { get; set; }  

        public string? Name { get; set; }


        public bool? IsBestSeller { get; set; }

        public bool? IsHot { get; set; }

        public bool? IsNew {  get; set; }

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
        public bool? IsActive { get; set; }

        public string? Description { get; set; }
    }
}
