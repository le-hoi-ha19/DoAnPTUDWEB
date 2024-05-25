using System.Security.Cryptography;
using System.Text;

namespace DoAn_PTUDWEB.Utilities
{
	public class Functions
	{
		//Ví dụ, nếu bạn gọi hàm TitleSlugGeneration("article", "Tiêu đề bài viết", 123)
		//thì kết quả trả về có thể là "article-tieu-de-bai-viet-123.html".
		public static string TitleSlugGeneration(string type, string title, int id)
		{
			string sTitle = type + "-" + SlugGenerator.SlugGenerator.GenerateSlug(title) + "-" + id.ToString();
			return sTitle;
		}

		// hàm lấy ngày tháng năm và thời gian hiện tại
		public static string getCurrentDate()
		{
			return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
		}
		// chuyển đổi mật khẩu thành một chuỗi ký tự hex được mã hóa bằng thuật toán MD5
		public static string MD5Hash(string text)
		{
			MD5 md5 = new MD5CryptoServiceProvider();
			md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));
			byte[] result = md5.Hash;
			StringBuilder strBuilder = new StringBuilder();
			for (int i = 0; i < result.Length; i++)
			{
				strBuilder.Append(result[i].ToString("x2"));
			}
			return strBuilder.ToString();
		}

		// mã hóa thêm 5 lần nữa cho chuỗi mật khẩu đã được mã hóa ban đầu
		// mỗi lần lặp nhân đôi xâu mã hóa, ở giữa thêm "_"
		public static string MD5Password(string? text)
		{
			// gọi hàm MD5Hash để nhận vào mật khẩu để mã hóa
			// và trả ra chuỗi mật khẩu đã được mã hóa
			string str = MD5Hash(text);
			for (int i = 0; i <= 5; i++)

				str = MD5Hash(str + "_" + str);
			return str;
		}
	}
}
