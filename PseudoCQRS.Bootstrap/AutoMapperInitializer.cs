using System;
using System.Linq;
using System.Reflection;
using AutoMapper;

namespace PseudoCQRS.Bootstrap
{
	internal static class AutoMapperInitializer
	{
		public static void Initialise(Assembly automapperProfilesAssembly)
		{
			var profiles = automapperProfilesAssembly.GetTypes().Where( x => x.BaseType == typeof( Profile ) );
			Mapper.Initialize( x =>
			{
				foreach ( var profile in profiles )
					x.AddProfile( (Profile)Activator.CreateInstance( profile ) );
			} );
		}
	}
}
