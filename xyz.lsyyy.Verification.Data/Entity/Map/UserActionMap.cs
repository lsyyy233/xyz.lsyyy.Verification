using System;

namespace xyz.lsyyy.Verification.Data
{
	public class UserActionMap : BaseMap
	{
		public Guid UserId { get; set; }

		public virtual User User { get; set; }
	}
}
