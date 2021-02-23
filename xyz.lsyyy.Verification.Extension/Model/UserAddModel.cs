using System;

namespace xyz.lsyyy.Verification.Extension
{
	public class UserAddModel
	{
		/// <summary>
		/// 用户名
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// 所属职位Id
		/// </summary>
		public Guid PositionId { get; set; }

		/// <summary>
		/// 密码
		/// </summary>
		public string Password { get; set; }
	}
}
