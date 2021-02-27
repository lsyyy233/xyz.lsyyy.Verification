using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using xyz.lsyyy.Verification.Protos;

namespace xyz.lsyyy.Verification.Extension.Service
{
	public class UserService
	{
		private readonly UserRpcService.UserRpcServiceClient userClient;
		private readonly ILogger<UserService> log;
		private UserModel User;
		public Guid UserId { get; private set; } = Guid.Empty;

		public UserService(UserRpcService.UserRpcServiceClient userClient, ILoggerFactory loggerFactory)
		{
			log = loggerFactory.CreateLogger<UserService>();
			this.userClient = userClient;
		}

		/// <summary>
		/// 设置用户Id
		/// </summary>
		/// <param name="UserIdStr"></param>
		internal void SetUserId(string UserIdStr)
		{
			if (UserId == Guid.Empty)
			{
				Guid.TryParse(UserIdStr, out Guid userId);
				UserId = userId;
			}
		}

		/// <summary>
		/// 注册用户
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="model"></param>
		/// <returns></returns>
		public async Task<GeneralResponse> RegistUserAsync(UserRegistModel model)
		{
			RegistUserRequest user = new RegistUserRequest
			{
				Name = model.Name,
				Password = model.Password,
				PositionId = model.PositionId.ToString()
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
			GeneralResponse response = await userClient.RegistAdminUserAsync(new RegistAdminUserRequest
			{
				CurrentUserId = UserId.ToString(),
				UserName = model.Name,
				Password = model.Password
			});
			return response;
		}

		/// <summary>
		/// 从服务端查询用户信息
		/// </summary>
		/// <param name="userId"></param>
		/// <returns></returns>
		public async Task<UserModel> GetUserInfoAsync()
		{
			if (User != null)
			{
				return User;
			}
			GetUserResponse response = await userClient.GetUserAsync(new GeneralIdRequest
			{
				Id = UserId.ToString()
			});
			Guid.TryParse(response.Id, out Guid UserId_);
			Guid? PositionId = null;
			if (string.IsNullOrWhiteSpace(response.PositionId))
			{
				if (Guid.TryParse(response.PositionId, out Guid pId))
				{
					PositionId = pId;
				}
			}
			User = new UserModel
			{
				UserId = UserId_,
				UserName = response.Name,
				PositionId = PositionId
			};
			return User;
		}
	}
}
