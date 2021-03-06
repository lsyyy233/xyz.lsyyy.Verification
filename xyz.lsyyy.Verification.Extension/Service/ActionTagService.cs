using Grpc.Core;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xyz.lsyyy.Verification.Protos;

namespace xyz.lsyyy.Verification.Extension.Service
{
	public class ActionTagService
	{
		private readonly ActionRpcService.ActionRpcServiceClient actionRpcClient;
		private readonly AuthorizationTagService authorizationTagService;
		public ActionTagService(ActionRpcService.ActionRpcServiceClient actionRpcClient, AuthorizationTagService authorizationTagService)
		{
			this.actionRpcClient = actionRpcClient;
			this.authorizationTagService = authorizationTagService;
		}

		/// <summary>
		/// 从服务端获取全部Tag
		/// </summary>
		/// <returns></returns>
		public async Task<IEnumerable<ActionTagModel>> GetAllTagAsync()
		{
			List<ActionTagModel> result = new List<ActionTagModel>();
			AsyncServerStreamingCall<GetAllTagResponse> call = actionRpcClient.GetAllTag(new GetAllTagRequest());
			IAsyncStreamReader<GetAllTagResponse> streamReader = call.ResponseStream;
			while (await streamReader.MoveNext())
			{
				GetAllTagResponse tagResponse = streamReader.Current;
				result.Add(new ActionTagModel
				{
					Id = tagResponse.TagId,
					ActionName = tagResponse.ActionName,
					ControllerName = tagResponse.ControllerName,
					Tag = tagResponse.TagName
				});
			}
			return result;
		}

		/// <summary>
		/// 获取Tag状态
		/// </summary>
		/// <returns></returns>
		public async Task<ActionTagStatus> GetTagStatus()
		{
			IEnumerable<ActionTagModel> actionTags = await GetAllTagAsync();
			IEnumerable<ActionTagMap> memActionTags = authorizationTagService.map;

			ActionTagStatus status = new ActionTagStatus
			{
				Normal =
					from at in actionTags
					join mat in memActionTags on at.Tag equals mat.Tag
					where mat.ActionName == at.ActionName && mat.ControllerName == at.ControllerName
					select at,
				Deleted =
					from at in actionTags
					where !memActionTags.Select(x => x.Tag).Contains(at.Tag)
					select at,
				New =
					from mat in memActionTags
					where !actionTags.Select(x => x.Tag).Contains(mat.Tag)
					select mat,
				Modified =
					from at in actionTags
					join mat in memActionTags on at.Tag equals mat.Tag
					where mat.ActionName != at.ActionName || mat.ControllerName != at.ControllerName
					select new ModifiedActionTag
					{
						New = mat,
						Old = at
					}
			};
			return status;
		}
	}
}
