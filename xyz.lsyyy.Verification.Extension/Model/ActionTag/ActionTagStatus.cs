using System.Collections.Generic;
using Castle.Core.Internal;

namespace xyz.lsyyy.Verification.Extension
{
	public class ActionTagStatus
	{
		public bool IsClean => Deleted.IsNullOrEmpty() && New.IsNullOrEmpty() && Modified.IsNullOrEmpty();

		/// <summary>
		/// 正常
		/// </summary>
		public IEnumerable<ActionTagModel> Normal { get; set; }

		/// <summary>
		/// 已删除
		/// </summary>
		public IEnumerable<ActionTagModel> Deleted { get; set; }

		/// <summary>
		/// 新增
		/// </summary>
		public IEnumerable<ActionTagMap> New { get; set; }

		/// <summary>
		/// 已修改
		/// </summary>
		public IEnumerable<ModifiedActionTag> Modified { get; set; }
	}

	public class ModifiedActionTag
	{
		/// <summary>
		/// 新
		/// </summary>
		public ActionTagMap New { get; set; }

		/// <summary>
		/// 旧
		/// </summary>
		public ActionTagModel Old { get; set; }
	}
}
