using System.Security.Cryptography;
using System.Text;

namespace DoAn_PTUDWEB.Utilities
{
	public class Functions
	{
		//_UserID: có tác dụng lấy ID của người dùng đang đăng nhập và check đăng nhập
		public static int _UserID = 0;
		//_UserName: có tác dụng là hiển thị tên người dùng đăng nhập hiện tại trong Trang admin và check đăng nhập
		public static string _UserName = string.Empty;
		//_Message: có tác dụng là hiển thị thông báo lỗi khi người dùng nhập tài khoản hoặc mk
		public static string _Message = string.Empty;
		//_MessageEmail: có tác dụng là hiển thị thông báo lỗi khi Tên tài khoản đã được đăng ký(bị trùng)
		public static string _MessageUserName = string.Empty;
		public static string _Email = string.Empty;
		public static string _Address = string.Empty;
		public static string _FullName = string .Empty;
		public static int _RoleId = 0;





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


		// nếu 2 biến là: _UserName là rỗng và biến _UserID <= 0 thì là chưa đăng nhập=> trả về false
		// ngược lại thì đã đăng nhập=> trả về true
		public static bool IsLogin()
		{
			if (string.IsNullOrEmpty(Functions._UserName) ||  (Functions._UserID <= 0))
				return false;
			return true;
		}
		public static bool CheckAdminPermission()
		{
			if (Functions._RoleId == 1)
				return true;
			return false;
		}
		public static bool CheckCustomerPermission()
		{
			if (Functions._RoleId == 2)
				return true;
			return false;
		}
		
		
		


	}
}
