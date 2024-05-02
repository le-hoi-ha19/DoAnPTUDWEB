namespace DoAn_PTUDWEB.Models.ViewModels
{
	public class ProductViewModel
	{
		public int? ProductId { get; set; }
		public string? Name { get; set; }
		public int? Price { get; set; }
		public int? ProductColorId { get; set; }
		public string? ProductColor { get; set; }
		public int? ProductTypeId { get; set; }
		public string? ProductType { get; set;}
		public int? Quantity { get; set; }
	}
}
