using Newtonsoft.Json;

namespace xyz.lsyyy.Verification.Util
{
	public static class WebResultHelper
	{
		public static string JsonResult(object obj)
		{
			return JsonConvert.SerializeObject(obj);
		}

		public static string JsonMessageResult(string Message)
		{
			return JsonConvert.SerializeObject(new
			{
				Message
			});
		}
	}
}
