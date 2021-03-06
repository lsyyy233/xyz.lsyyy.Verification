using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using xyz.lsyyy.Verification.Protos;

namespace xyz.lsyyy.Verification.Extension
{
	public class AuthorizationTagService
	{
		private readonly ActionRpcService.ActionRpcServiceClient actionRpcService;
		public readonly IEnumerable<ActionTagMap> map;
		private readonly ILogger<AuthorizationTagService> log;


		//获取所有带AuthorizationTagAttribute的action，将controllerName和actionName以及tag信息添加到集合中
		public AuthorizationTagService(
			IActionDescriptorCollectionProvider actionProvider,
			ActionRpcService.ActionRpcServiceClient actionRpcService,
			ILoggerFactory loggerFactory)
		{
			log = loggerFactory.CreateLogger<AuthorizationTagService>();
			this.actionRpcService = actionRpcService;
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

		/// <summary>
		/// 查询将要访问Action是否有Tag
		/// </summary>
		/// <param name="controllerName"></param>
		/// <param name="actionName"></param>
		/// <returns></returns>
		public bool ActionHasTag(string controllerName, string actionName)
		{
			if (map.Any(x => x.ActionName.Equals(actionName) && x.ControllerName == controllerName))
			{
				return true;
			}
			return false;
		}

		/// <summary>
		/// 获取访问的Action的Tag
		/// </summary>
		/// <param name="controllerName"></param>
		/// <param name="actionName"></param>
		/// <returns></returns>
		public string GetTagName(string controllerName, string actionName)
		{
			return map
				.FirstOrDefault(x => x.ControllerName == controllerName && x.ActionName == actionName)
				?.Tag;
		}
	}
}
