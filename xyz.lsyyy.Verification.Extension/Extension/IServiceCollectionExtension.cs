using Grpc.Net.ClientFactory;
using Microsoft.Extensions.DependencyInjection;
using System;
using xyz.lsyyy.Verification.Extension.Service;

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
			services.AddGrpcClient<Protos.UserRpcService.UserRpcServiceClient>(grpcOptionAction);
			services.AddGrpcClient<Protos.VerificationRpcService.VerificationRpcServiceClient>(grpcOptionAction);
			services.AddGrpcClient<Protos.UserRpcService.UserRpcServiceClient>(grpcOptionAction);
			services.AddGrpcClient<Protos.ActionRpcService.ActionRpcServiceClient>(grpcOptionAction);
		}
	}
}
