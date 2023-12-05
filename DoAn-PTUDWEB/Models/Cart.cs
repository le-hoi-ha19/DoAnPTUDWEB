namespace DoAn_PTUDWEB.Models
{
		public class Cart
		{
			public List<CartLine> Lines { get; set; } = new List<CartLine>();
			public void AddItem(TbProduct product, int quantity)
			{
				CartLine? line = Lines
				.Where(p => p.Product.ProductId == product.ProductId)
				.FirstOrDefault();
				if (line == null)
				{
					Lines.Add(new CartLine()
					{
						Product = product,
						Quantity = quantity
					});
				}
				else
				{
					line.Quantity += quantity;
					//Bắt lỗi nếu trừ xuống đến giá trị âm ở đây
					if(line.Quantity < 0)
					{
						RemoveLine(product);
                    }
				}
			}

			public void RemoveLine(TbProduct product) =>
			 Lines.RemoveAll(l => l.Product.ProductId == product.ProductId);

		public decimal ComputeTotalValue()
		{
			return (decimal)Lines.Sum(e => e.Product?.Price * (1 - e.Product?.PriceDiscount) * e.Quantity);
		}

		public void Clear() => Lines.Clear();
		}

		//CartLine là mỗi đơn hàng(ngang)
		public class CartLine
		{
			public int CartLineId { get; set; }
			public TbProduct Product { get; set; } = new();
			public int Quantity { get; set; }
		}
	
}
