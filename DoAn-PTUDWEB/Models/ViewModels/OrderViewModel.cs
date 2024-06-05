namespace DoAn_PTUDWEB.Models.ViewModels
{
	public class OrderViewModel
	{
		public int? OrderId { get; set; }
		public DateTime? OrderDate { get; set; }
		public string? CustomerName { get; set; }
		public string? Phone { get; set; }
		public string? Email { get; set; }
		public string? ShipAddress { get; set; }
		public string? Note { get; set; }
		public int? Status { get; set; }
        public bool IsPayment { get; set; }

        public List<OrderDetailViewModel>? Details { get; set; }
	}
}
