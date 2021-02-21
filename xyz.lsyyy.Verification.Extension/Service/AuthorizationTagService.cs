using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;

namespace xyz.lsyyy.Verification.Extension
{
	public class AuthorizationTagService
	{
		private readonly IEnumerable<ActionTagMap> map;
		//获取所有带AuthorizationTagAttribute的action，将controllerName和actionName以及tag信息添加到集合中
		public AuthorizationTagService(IActionDescriptorCollectionProvider actionProvider)
		{
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
