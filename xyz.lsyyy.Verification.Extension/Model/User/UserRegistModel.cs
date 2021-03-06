﻿using System;
using xyz.lsyyy.Verification.Util;

namespace xyz.lsyyy.Verification.Extension
{
	public class UserRegistModel
	{
		/// <summary>
		/// 用户名
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// 所属职位Id
		/// </summary>
		public int PositionId { get; set; }

		/// <summary>
		/// 密码
		/// </summary>
		public string Password
		{
			get => _password; 
			set => _password = HashHelper.GetSha256WithSalt(value);
		}

		private string _password;
	}
}
