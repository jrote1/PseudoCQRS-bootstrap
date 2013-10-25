using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;

namespace PseudoCQRS.Bootstrap.Demo.Modules.Users.Add
{
	public class UserAddMappingProfile : Profile
	{
		protected override void Configure()
		{
			CreateMap<UserAddViewModel, UserAddCommand>();
		}
	}
}