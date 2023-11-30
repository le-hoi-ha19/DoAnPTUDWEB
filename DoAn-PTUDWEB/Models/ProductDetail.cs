namespace DoAn_PTUDWEB.Models
{
	public class ProductDetail
	{
		public TbProduct Product { get; set; }
		public List<TbImageProduct> Images { get; set; }
		public List<TbColor> Colors { get; set; }
		public List<TbProduct> relatedProducts { get; set; }


	}
}
