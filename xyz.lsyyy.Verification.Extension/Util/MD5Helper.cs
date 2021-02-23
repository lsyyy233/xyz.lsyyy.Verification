using System.Security.Cryptography;
using System.Text;

namespace xyz.lsyyy.Verification.Extension.Util
{
	public static class MD5Helper
	{
		private const string salt1 = "1q%a2w&s3e?d%$#";
		private const string salt2 = "cbyv&*^#bfjh)*^#";
		public static string GetMd5WithSalt(string str)
		{
			return GetMd5(salt1 + str + salt2);
		}
		public static string GetMd5(string str)
		{
			StringBuilder md5Str = new StringBuilder();
			byte[] data = Encoding.GetEncoding("utf-8").GetBytes(str);
			MD5 md5 = new MD5CryptoServiceProvider();
			byte[] bytes = md5.ComputeHash(data);
			for (int i = 0; i < bytes.Length; i++)
			{
				md5Str.Append(bytes[i].ToString("x2"));
			}
			return md5Str.ToString();
		}
	}
}
