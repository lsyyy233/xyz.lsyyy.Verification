using System;

namespace xyz.lsyyy.Verification.Data
{
	public class PositionActionMap : BaseMap
	{
		public Guid PositionId { get; set; }

		public virtual Position Position { get; set; }
	}
}
