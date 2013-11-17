using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using PseudoCQRS.Bootstrap.DataAccess;

namespace PseudoCQRS.Bootstrap
{
	internal static class UnityRegistrar
	{
		public static void Register( IUnityContainer container, Assembly viewModelProvidersAssembly )
		{
			RegisterCQRSImplementations( container );
			RegisterDependenciesInCurrentProject( container );

			ServiceLocator.SetLocatorProvider( () => new UnityServiceLocator( container ) );
			container.RegisterInstance<IServiceLocator>( ServiceLocator.Current );
		}

		private static void RegisterDependenciesInCurrentProject( IUnityContainer container )
		{
			container.RegisterType<IRepository, Repository>();
		}

		private static void RegisterCQRSImplementations( IUnityContainer container )
		{
			container.RegisterType<IEventSubscriberAssembliesProvider, EventSubscriberAssembliesProvider>();
		}
	}
}
