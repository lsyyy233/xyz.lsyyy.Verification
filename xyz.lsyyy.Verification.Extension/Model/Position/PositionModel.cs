using System;

namespace xyz.lsyyy.Verification.Extension.Model.Position
{
	public class PositionModel
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public int? SuperiorPositionId { get; set; }

		public int DepartmentId { get; set; }
	}
}
