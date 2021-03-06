using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using xyz.lsyyy.Verification.Protos;
using static System.String;

namespace xyz.lsyyy.Verification.Extension.Service
{
	public class UserService
	{
		private readonly UserRpcService.UserRpcServiceClient userClient;

		private readonly ILogger<UserService> log;

		private UserModel User;
		public string Token { get; private set; } = Empty;

		public UserService(UserRpcService.UserRpcServiceClient userClient, ILoggerFactory loggerFactory)
		{
			log = loggerFactory.CreateLogger<UserService>();
			this.userClient = userClient;
		}

		/// <summary>
		/// 设置用户Id
		/// </summary>
		/// <param name="Id"></param>
		internal void SetToken(string token)
		{
			if (IsNullOrWhiteSpace(Token))
			{
				Token = token;
			}
		}

		/// <summary>
		/// 注册用户
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		public async Task<GeneralResponse> RegistUserAsync(UserRegistModel model)
		{
			RegistUserRequest user = new RegistUserRequest
			{
				Name = model.Name,
				Password = model.Password,
				PositionId = model.PositionId
			};
			GeneralResponse result = await userClient.RegistUserAsync(user);
			return result;
		}

		/// <summary>
		/// 用户登录
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		public async Task<UserLoginResponse> UserLoginAsync(LoginModel model)
		{
			UserLoginResponse result = await userClient.UserLoginAsync(new UserLoginRequest
			{
				UserName = model.Name,
				Password = model.Password
			});
			return result;
		}

		/// <summary>
		/// 注册管理员用户
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		public async Task<GeneralResponse> RegistAdminAsync(AdminRegistModel model)
		{
			RegistAdminUserRequest request = new RegistAdminUserRequest
			{
				CurrentUserId = null,
				UserName = model.Name,
				Password = model.Password
			};
			if (!IsNullOrWhiteSpace(Token))
			{
				request.CurrentUserId = (await GetCurrentUserInfoAsync()).UserId;
			}
			GeneralResponse response = await userClient.RegistAdminUserAsync(request);
			return response;
		}

		/// <summary>
		/// 从服务端查询当前登录用户信息
		/// </summary>
		/// <param name="userId"></param>
		/// <returns></returns>
		public async Task<UserModel> GetCurrentUserInfoAsync()
		{
			if (User != null)
			{
				return User;
			}
			User = await GetUserInfoAsync(Token);
			return User;
		}

		public async Task<UserModel> GetUserInfoAsync(string Token)
		{
			GetUserResponse response = await userClient.GetCurrentUserAsync(new GetUserRequesr
			{
				Token = Token
			});
			return new UserModel
			{
				UserId = response.Id,
				UserName = response.Name,
				PositionId = response.PositionId
			};
		}
	}
}
