using System;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using xyz.lsyyy.Verification.Data;
using xyz.lsyyy.Verification.Protos;

namespace xyz.lsyyy.Verification
{
	/// <summary>
	/// GRPC服务端
	/// </summary>
	public class UserService : Protos.User.UserBase
	{
		private readonly MyDbContext db;
		public UserService(MyDbContext db)
		{
			this.db = db;
		}

		public override async Task<RegistUserResult> RegistUser(RegistUserModel request, ServerCallContext context)
		{
			//职位id不存在或者用户名为空，返回失败
			Guid PositionId = Guid.Parse(request.PositionId);
			if (!await db.Positions.AnyAsync(x => x.Id == PositionId) || string.IsNullOrWhiteSpace(request.Name))
			{
				return new RegistUserResult
				{
					Success = false
				};
			}
			await db.Users.AddAsync(new Data.User
			{
				Id = new Guid(),
				Name = request.Name,
				PositionId = PositionId
			});
			int result = await db.SaveChangesAsync();
			if (result <= 0)
			{
				return new RegistUserResult
				{
					Success = false
				};
			}
			return new RegistUserResult
			{
				Success = true
			};
		}
	}
}
