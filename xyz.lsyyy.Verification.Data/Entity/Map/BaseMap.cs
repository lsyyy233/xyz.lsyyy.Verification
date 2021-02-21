using System;

namespace xyz.lsyyy.Verification.Data
{
	public class BaseMap
	{
		public Guid ActionId { get; set; }

		public virtual Action Action { get; set; }
	}
}
