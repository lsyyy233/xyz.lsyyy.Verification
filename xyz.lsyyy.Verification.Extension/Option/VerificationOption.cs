using Microsoft.AspNetCore.Http;
using System;

namespace xyz.lsyyy.Verification.Extension.Option
{
	public class VerificationOption
	{
		public Func<HttpContext, Guid> getUserIdFunc { get; private set; }

	}
}
