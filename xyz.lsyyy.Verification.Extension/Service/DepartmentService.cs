using System.Threading.Tasks;
using xyz.lsyyy.Verification.Protos;

namespace xyz.lsyyy.Verification.Extension.Service
{
	public class DepartmentService
	{
		private readonly DepartmentRpcService.DepartmentRpcServiceClient departmentRpcClient;

		private int? departmentId;
		private DepartmentModel department;

		public DepartmentService(DepartmentRpcService.DepartmentRpcServiceClient departmentRpcClient)
		{
			this.departmentRpcClient = departmentRpcClient;
		}

		internal void SetDepartmentId(int id)
		{
			if (departmentId == null)
			{
				departmentId = id;
			}
		}

		public async Task<DepartmentModel> GetCurrentUserDepartmentAsync()
		{
			if (department == null)
			{
				department = await GetDepartmentAsync(departmentId ?? 0);
			}
			return department;
		}

		/// <summary>
		/// 查询部门
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public async Task<DepartmentModel> GetDepartmentAsync(int id)
		{
			GetDepartmentResponse response = await departmentRpcClient.GetDepartmentAsync(new GeneralIdRequest
			{
				Id = id
			});
			DepartmentModel department = new DepartmentModel
			{
				DepartmentId = response.DepartmentId,
				DepartmentName = response.DepartmentName,
				SuperiorDepartmentId = response.SuperiorDepartmentId
			};
			return department;
		}

		/// <summary>
		/// 添加部门
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		public async Task<GeneralResponse> AddDepartmentAsync(DepartmentAddModel model)
		{
			GeneralResponse response = await departmentRpcClient.AddDepartmentAsync(new AddDepartmentRequest
			{
				DepartmentName = model.DepartmentName,
				SuperiorDepartmentId = model.SuperiorDepartmentId
			});
			return response;
		}
	}
}
