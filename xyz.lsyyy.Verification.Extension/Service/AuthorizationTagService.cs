using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using xyz.lsyyy.Verification.Protos;

namespace xyz.lsyyy.Verification.Extension
{
	public class AuthorizationTagService
	{
		private readonly ActionService.ActionServiceClient actionService;
		public readonly IEnumerable<ActionTagMap> map;
		private readonly ILogger<AuthorizationTagService> log;


		//获取所有带AuthorizationTagAttribute的action，将controllerName和actionName以及tag信息添加到集合中
		public AuthorizationTagService(
			IActionDescriptorCollectionProvider actionProvider,
			ActionService.ActionServiceClient actionService,
			ILoggerFactory loggerFactory)
		{
			log = loggerFactory.CreateLogger<AuthorizationTagService>();
			this.actionService = actionService;
			map = actionProvider.ActionDescriptors.Items.Cast<ControllerActionDescriptor>()
				.Where(x =>
					x.MethodInfo.CustomAttributes
						.Any(y => y.AttributeType == typeof(AuthorizationTagAttribute))
				)
				.Select(x =>
					new ActionTagMap
					{
						ActionName = x.ActionName,
						ControllerName = x.ControllerName,
						Tag = (string)x.MethodInfo.CustomAttributes
							.Where(y => y.AttributeType == typeof(AuthorizationTagAttribute))
							.Select(y => (y.NamedArguments ?? throw new ArgumentNullException())
								.Select(z => z.TypedValue)
								.FirstOrDefault())
							.FirstOrDefault().Value
					});
		}

		public void PushActionMap()
		{
			Task.Run(async () =>
			{
				using AsyncClientStreamingCall<ActionRequest, ActionResponse> call = actionService.PushActions();
				IClientStreamWriter<ActionRequest> stream = call.RequestStream;
				foreach (ActionTagMap m in map)
				{
					await stream.WriteAsync(new ActionRequest
					{
						ActionName = m.ActionName,
						ControllerName = m.ControllerName,
						Tag = m.Tag
					});
				}
				await stream.CompleteAsync();
				ActionResponse response = await call.ResponseAsync;
				log.LogTrace($"Action Server status : {response.Status}");

			});
		}

		public bool ActionHasTag(string controllerName, string actionName)
		{
			if (map.Any(x => x.ActionName.Equals(actionName) && x.ControllerName == controllerName))
			{
				return true;
			}
			return false;
		}

		public string GetTagName(string controllerName, string actionName)
		{
			return map
				.FirstOrDefault(x => x.ControllerName == controllerName && x.ActionName == actionName)
				?.Tag;
		}
	}
}
