using System;

namespace xyz.lsyyy.Verification.Extension
{
	public class DepartmentModel
	{
		public int DepartmentId { get; set; }

		public string DepartmentName { get; set; }

		public int? SuperiorDepartmentId { get; set; }
	}
}
