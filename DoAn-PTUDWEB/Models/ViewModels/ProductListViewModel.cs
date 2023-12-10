namespace DoAn_PTUDWEB.Models.ViewModels
{
    public class ProductListViewModel
    {
        public IEnumerable<TbProduct> TbProducts { get; set; } = Enumerable.Empty<TbProduct>();
        public PagingInfo PagingInfo { get; set; } = new PagingInfo();
    }
}
