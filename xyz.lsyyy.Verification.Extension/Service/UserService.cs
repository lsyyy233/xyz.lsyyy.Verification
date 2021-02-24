using System.Threading.Tasks;
using xyz.lsyyy.Verification.Protos;

namespace xyz.lsyyy.Verification.Extension.Service
{
	public class UserService
	{
		private readonly User.UserClient userClient;
		public UserService(User.UserClient userClient)
		{
			this.userClient = userClient;
		}
		public async Task<RegistUserResponse> RegistUserAsync<T>(T model) where T : UserAddModel
		{
			RegistUserRequest user = new RegistUserRequest
			{
				Name = model.Name,
				Password = model.Password,
				PositionId = model.PositionId.ToString()
			};
			RegistUserResponse result = await userClient.RegistUserAsync(user);
			return result;
		}

		public async Task<LoginResponse> UserLoginAsync(LoginModel model)
		{
			LoginResponse result = await userClient.UserLoginAsync(new Protos.LoginRequest
			{
				Name = model.Name,
				Password = model.Password
			});
			return result;
		}
	}
}
