namespace DoAn_PTUDWEB.Models
{
    public class InformationOrder
    {
        public List<TbProduct> Product { get; set; }
        public List<TbOrder> Order { get; set; }
        public List<TbOrderDetail> OrderDetails { get; set; }
        public List<TbUser> User { get; set; }

    }
}
