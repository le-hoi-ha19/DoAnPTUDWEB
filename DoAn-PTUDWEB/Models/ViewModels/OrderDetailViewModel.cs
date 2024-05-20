namespace DoAn_PTUDWEB.Models.ViewModels
{
	public class OrderDetailViewModel
	{
		public int? ProductId { get; set; }
		public string? Name { get; set; }
		public string? ProductImage { get; set; }
		public decimal? Price { get; set; }
		public int? ProductColorId { get; set; }
		public string? ProductColor { get; set; }
		public int? ProductTypeId { get; set; }
		public string? ProductType { get; set; }
		public int? Quantity { get; set; }
		public decimal? TotalMoney { get; set;}
	}
}
