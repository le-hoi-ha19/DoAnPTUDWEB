using System.Text.Json;

namespace DoAn_PTUDWEB.Infrastructure
{
	public static class SessionExtensions
	{
		// Lưu trữ đối tượng vào session
		public static void SetJson(this ISession session, string key, object value)
		{
			session.SetString(key, JsonSerializer.Serialize(value));
		}

		// lấy  đối tượng từ session
		public static T? GetJson<T>(this ISession session, string key)
		{
			var sessionData = session.GetString(key);
			return sessionData == null
			? default(T) : JsonSerializer.Deserialize<T>(sessionData);
		}
	}
}
