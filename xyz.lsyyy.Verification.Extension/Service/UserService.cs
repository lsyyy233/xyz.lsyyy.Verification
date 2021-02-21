using Grpc.Core;
using xyz.lsyyy.Verification.Protos;

namespace xyz.lsyyy.Verification.Extension.Service
{
	public class UserService : Protos.User.UserClient
	{
		public override RegistUserResult RegistUser(RegistUserModel request, CallOptions options)
		{
			return new RegistUserResult
			{
				Success = true
			};
		}
	}
}
