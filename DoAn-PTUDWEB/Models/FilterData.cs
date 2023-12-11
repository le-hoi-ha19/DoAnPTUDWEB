namespace DoAn_PTUDWEB.Models
{
	public class FilterData
	{

		public string PriceRange { get; set; } = null!;
		public List<string> Colors { get; set; } = null!;
		public List<string> Trademarks { get; set; } = null!;
		public List<int> Categories { get; set; } = null!;
	}
}
