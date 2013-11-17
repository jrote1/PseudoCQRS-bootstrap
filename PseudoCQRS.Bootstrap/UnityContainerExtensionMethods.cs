using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.Practices.Unity;

namespace PseudoCQRS.Bootstrap
{
	public static class UnityContainerExtensionMethods
	{
		public static void RegisterNonGenericImplementationsWithITypeName( this IUnityContainer container, Assembly inAssembly )
		{
			var typesWithITypeInterfaces = inAssembly.GetTypes()
												   .Where( x => x.IsClass && !x.IsAbstract && x.IsPublic )
												   .Where( x => x.FindInterfaces( ( y, z ) => y.Name == "I" + x.Name, null ).Any() );

			foreach ( var implementation in typesWithITypeInterfaces )
			{
				var interfaceType = implementation.GetInterface( "I" + implementation.Name );
				container.RegisterType( interfaceType, implementation );
			}
		}

	}
}
