using System;
using System.Security.Cryptography;
using System.Text;

namespace xyz.lsyyy.Verification.Util
{
	public static class HashHelper
	{
		private const string salt1 = "1q%a2w&s3e?d%$#";
		private const string salt2 = "cbyv&*^#bfjh)*^#";

		public static string GetSha256WithSalt(string str)
		{
			return GetHash(salt1 + str + salt2);
		}

		public static string GetHash(string str)
		{
			using SHA256 sha256Hash = SHA256.Create();
			Byte[] data = Encoding.UTF8.GetBytes(str);
			byte[] hashByteData = sha256Hash.ComputeHash(data);
			StringBuilder sBuilder = new StringBuilder();
			foreach (byte t in hashByteData)
			{
				sBuilder.Append(t.ToString("x2"));
			}
			return sBuilder.ToString();
		}
	}
}
