﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PseudoCQRS.Bootstrap.Demo.Modules.Users.Add
{
	public class UserAddCommand
	{
		public string Name { get; set; }
		public string EmailAddress { get; set; }
		public string Password { get; set; }
	}
}