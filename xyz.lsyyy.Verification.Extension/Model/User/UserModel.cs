using System;

namespace xyz.lsyyy.Verification.Extension
{
	public class UserModel
	{
		/// <summary>
		/// 用户Id
		/// </summary>
		public Guid UserId { get; set; }

		/// <summary>
		/// 应户名
		/// </summary>
		public string UserName { get; set; }

		/// <summary>
		/// 职位Id
		/// </summary>
		public Guid? PositionId { get; set; }
	}
}
