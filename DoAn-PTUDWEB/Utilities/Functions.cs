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

	}
}
