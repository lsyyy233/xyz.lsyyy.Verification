using System.Threading.Tasks;
using xyz.lsyyy.Verification.Extension.Model.Position;
using xyz.lsyyy.Verification.Protos;

namespace xyz.lsyyy.Verification.Extension
{
	public class PositionService
	{
		private readonly PositionRpcService.PositionRpcServiceClient positionRpcClient;

		public int? PositionId { get; private set; }
		private PositionModel position;

		public PositionService(PositionRpcService.PositionRpcServiceClient positionRpcClient)
		{
			this.positionRpcClient = positionRpcClient;
		}

		internal void SetPositionId(int? Id)
		{
			if (!PositionId.HasValue)
			{
				PositionId = Id;
			}
		}

		/// <summary>
		/// 添加职位
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		public async Task<GeneralResponse> AddPositionAsync(PositionAddModel model)
		{
			GeneralResponse response = await positionRpcClient.AddPositionAsync(new AddPositionRequest
			{
				DepartmentId = model.DepartmentId,
				PositionName = model.PositionName,
				SuperiorPositionId = model.SuperiorPositionId
			});
			return response;
		}

		/// <summary>
		/// 获取当前用户的职位信息
		/// </summary>
		/// <returns></returns>
		public async Task<PositionModel> GetCurrentUserPositionAsync()
		{
			if (position != null)
			{
				return position;
			}

			PositionModel positionModel = await GetPositionAsync(PositionId.Value);
			position = positionModel;
			return position;
		}

		/// <summary>
		/// 查询职位信息
		/// </summary>
		/// <param name="Id"></param>
		/// <returns></returns>
		public async Task<PositionModel> GetPositionAsync(int Id)
		{
			GetPositionResponse response = await positionRpcClient.GetPositionAsync(new GeneralIdRequest
			{
				Id = Id
			});
			PositionModel positionModel = new PositionModel
			{
				Id = response.PositionId,
				DepartmentId = response.DepartmentId,
				Name = response.PositionName,
			};
			positionModel.SuperiorPositionId = response.SuperiorPositionId == -1 ? default : response.SuperiorPositionId;
			if (response.SuperiorPositionId == -1)
			{
				positionModel.SuperiorPositionId = null;
			}
			else
			{
				positionModel.SuperiorPositionId = response.SuperiorPositionId;
			}
			return positionModel;
		}

	}
}
