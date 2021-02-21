using System.Threading.Tasks;
using Grpc.Core;
using xyz.lsyyy.Verification.Protos;

namespace xyz.lsyyy.Verification.Services
{
	public class VerificationService : Protos.Verification.VerificationBase
	{
		public override Task<VerificationResult> GetAccess(VerificationModel request, ServerCallContext context)
		{
			return Task.FromResult(new VerificationResult
			{
				Access = false
			});
		}
	}
}
