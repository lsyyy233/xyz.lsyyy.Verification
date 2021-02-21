using xyz.lsyyy.Verification.Protos;

namespace xyz.lsyyy.Verification.Extension.Service
{
	public class VerificationService
	{
		private readonly Protos.Verification.VerificationClient verificationClient;
		private readonly AuthorizationTagService tagService;

		public VerificationService(Protos.Verification.VerificationClient verificationClient, AuthorizationTagService tagService)
		{
			this.verificationClient = verificationClient;
			this.tagService = tagService;
		}
		

		/// <summary>
		/// 是否允许访问
		/// </summary>
		/// <param name="context"></param>
		/// <param name="GetUserIdFunc"></param>
		/// <returns></returns>
		public bool AllowAccess(string controllerName, string actionName, string UserId)
		{
			string tagName = tagService.GetTagName(controllerName,actionName);
			VerificationModel verification = new VerificationModel
			{
				TagName = tagName,
				UserId = UserId,
			};
			return verificationClient.GetAccess(verification).Access;
		}
	}
}
