using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace DoAn_PTUDWEB.Models
{
    // class Cart không có constructor hoặc constructor không có tham số nên có thể tạo 1 đối tượng mới bằng cách: new Cart();
    // nếu class có constructor có tham số thì phải truyền giá trị vào khi New đối tượng:ví dụ : new Person("Alice", 30)

    public class Cart
		{
			public List<CartLine> Lines { get; set; } = new List<CartLine>();
			public void AddItem(TbProduct product, int quantity, int colorId, int typeId,string ColorName, string TypeName)
			{
				CartLine? line = Lines
				.Where(p => p.Product.ProductId == product.ProductId && p.ColorId == colorId && p.TypeId == typeId)
				.FirstOrDefault();
				if (line == null)
				{
					Lines.Add(new CartLine()
					{
						Product = product,
						Quantity = quantity,
						ColorId = colorId,
						ColorName = ColorName,
						TypeId = typeId,
						TypeName = TypeName
					});
				}
				else
				{
				// phải check khác typeId và colorId

					line.Quantity += quantity;
				//Bắt lỗi nếu trừ xuống đến giá trị âm ở đây
					if(line.Quantity <= 0)
					{
						RemoveLine(product, colorId, typeId);
                    }
				}
			}

		public void RemoveLine(TbProduct product, int colorId, int typeId)
		{
			Lines.RemoveAll(l => l.Product.ProductId == product.ProductId && l.ColorId == colorId && l.TypeId == typeId);
		}

		public decimal ComputeTotalValue()
			{
				return (decimal)Lines.Sum(e => e.Product?.Price * (1 - e.Product?.PriceDiscount) * e.Quantity);
			}

		}

		//CartLine là mỗi đơn hàng(ngang)
		//CartlLine là class dùng để định nghĩa đối tượng muốn tạo ra
        public class CartLine
		{
			public int CartLineId { get; set; }
			public TbProduct Product { get; set; } = new();
			public int Quantity { get; set; }
			public int ColorId { get; set; }
			public string? ColorName { get; set; }
			public int TypeId { get; set; }
			public string? TypeName { get; set;}
		}
	
}
