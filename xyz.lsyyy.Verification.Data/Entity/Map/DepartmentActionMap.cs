using System;

namespace xyz.lsyyy.Verification.Data
{
	public class DepartmentActionMap : BaseMap
	{
		public Guid DepartmentId { get; set; }

		public virtual Department Department { get; set; }
	}
}
