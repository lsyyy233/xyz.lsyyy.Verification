using System;
using xyz.lsyyy.Verification.Util.CertUtil;

namespace xyz.lsyyy.Verification.Data
{
	/// <summary>
	/// 用户
	/// </summary>
	public class User
	{
		/// <summary>
		/// 用户Id
		/// </summary>
		public Guid Id { get; set; }

		/// <summary>
		/// 用户名
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// 所属职位Id
		/// </summary>
		public Guid PositionId { get; set; }

		/// <summary>
		/// 所属职位
		/// </summary>
		public virtual Position Position { get; set; }

		/// <summary>
		/// 密码
		/// </summary>
		public string Password
		{
			get => _password; 
			set => _password = MD5Helper.GetMd5WithSalt(value);
		}

		private string _password;
	}
}
