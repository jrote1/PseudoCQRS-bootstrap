using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;

namespace $rootnamespace$
{
	public static class AutoMapperInitializer
	{
		public static void Initialise()
		{
			var profiles = typeof( AutoMapperInitializer ).Assembly.GetTypes().Where( x => x.BaseType == typeof( Profile ) );
			Mapper.Initialize( x =>
			{
				foreach ( var profile in profiles )
					x.AddProfile( (Profile)Activator.CreateInstance( profile ) );
			} );
		}
	}
}
