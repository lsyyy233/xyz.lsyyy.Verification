using Grpc.Net.ClientFactory;
using Microsoft.Extensions.DependencyInjection;
using System;
using xyz.lsyyy.Verification.Extension.Service;
using xyz.lsyyy.Verification.Protos;

namespace xyz.lsyyy.Verification.Extension
{
	public static class IServiceCollectionExtension
	{
		public static void AddVerificationService(this IServiceCollection services,Action<IServiceProvider, GrpcClientFactoryOptions> grpcOptionAction)
		{
			services.AddSingleton<AuthorizationTagService>();
			services.AddScoped<VerificationService>();
			services.AddScoped<UserService>();
			services.AddScoped<DepartmentService>();
			services.AddScoped<PositionService>();
			services.AddScoped<ActionTagService>();
			services.AddGrpcClient<UserRpcService.UserRpcServiceClient>(grpcOptionAction);
			services.AddGrpcClient<VerificationRpcService.VerificationRpcServiceClient>(grpcOptionAction);
			services.AddGrpcClient<UserRpcService.UserRpcServiceClient>(grpcOptionAction);
			services.AddGrpcClient<ActionRpcService.ActionRpcServiceClient>(grpcOptionAction);
			services.AddGrpcClient<DepartmentRpcService.DepartmentRpcServiceClient>(grpcOptionAction);
			services.AddGrpcClient<PositionRpcService.PositionRpcServiceClient>(grpcOptionAction);
		}
	}
}
