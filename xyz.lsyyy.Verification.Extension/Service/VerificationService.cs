using xyz.lsyyy.Verification.Protos;

namespace xyz.lsyyy.Verification.Extension.Service
{
	public class VerificationService
	{
		private readonly VerificationRpcService.VerificationRpcServiceClient verificationClient;
		private readonly AuthorizationTagService tagService;
		private readonly UserService userService;

		public VerificationService(
			VerificationRpcService.VerificationRpcServiceClient verificationClient,
			AuthorizationTagService tagService, UserService userService)
		{
			this.verificationClient = verificationClient;
			this.tagService = tagService;
			this.userService = userService;
		}


		/// <summary>
		/// 是否允许访问
		/// </summary>
		/// <param name="controllerName"></param>
		/// <param name="actionName"></param>
		/// <returns></returns>
		public bool AllowAccess(string controllerName, string actionName)
		{
			string token = userService.Token;
			string tagName = tagService.GetTagName(controllerName, actionName);
			VerificationModel verification = new VerificationModel
			{
				Token = token,
				TagName = tagName,
			};
			return verificationClient.GetAccess(verification).Access;
		}
	}
}
