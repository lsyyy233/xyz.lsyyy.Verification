using System;
using Grpc.Net.ClientFactory;
using Microsoft.Extensions.DependencyInjection;
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
			services.AddGrpcClient<Protos.User.UserClient>(grpcOptionAction);
			services.AddGrpcClient<Protos.Verification.VerificationClient>(grpcOptionAction);
			services.AddGrpcClient<Protos.User.UserClient>(grpcOptionAction);
		}
	}
}
