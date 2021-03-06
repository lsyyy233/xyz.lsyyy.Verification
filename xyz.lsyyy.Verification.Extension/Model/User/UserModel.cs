using System;

namespace xyz.lsyyy.Verification.Extension
{
	public class UserModel
	{
		/// <summary>
		/// 用户Id
		/// </summary>
		public int UserId { get; set; }

		/// <summary>
		/// 应户名
		/// </summary>
		public string UserName { get; set; }

		/// <summary>
		/// 职位Id
		/// </summary>
		public int? PositionId { get; set; }
	}
}
