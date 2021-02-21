using System;

namespace xyz.lsyyy.Verification.Data
{
	/// <summary>
	/// 职位
	/// </summary>
	public class Position
	{
		/// <summary>
		/// 职位Id
		/// </summary>
		public Guid Id { get; set; }

		/// <summary>
		/// 所属部门Id
		/// </summary>
		public Guid DepartmentId { get; set; }

		/// <summary>
		/// 所属部门
		/// </summary>
		public virtual Department Department { get; set; }

		/// <summary>
		/// 职位名称
		/// </summary>
		public string Name { get; set; }
	}
}
