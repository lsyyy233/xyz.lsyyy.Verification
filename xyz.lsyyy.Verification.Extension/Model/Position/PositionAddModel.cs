namespace xyz.lsyyy.Verification.Extension
{
	/// <summary>
	/// 添加职位
	/// </summary>
	public class PositionAddModel
	{
		/// <summary>
		/// 职位名称
		/// </summary>
		public string PositionName { get; set; }

		/// <summary>
		/// 上级职位
		/// </summary>
		public int? SuperiorPositionId { get; set; }

		/// <summary>
		/// 所属部门
		/// </summary>
		public int DepartmentId { get; set; }
	}
}
