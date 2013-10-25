using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using PseudoCQRS.Bootstrap.DataAccess;
using PseudoCQRS.Controllers;

namespace PseudoCQRS.Bootstrap
{
	public static class UnityRegistrar
	{
		public static void Register( IUnityContainer container, Assembly viewModelProvidersAssembly )
		{
			RegisterPseudoCQRSTypes( container );
			RegisterCQRSImplementations( container );
			RegisterViewModelProviders( container, viewModelProvidersAssembly );
			RegisterDependenciesInCurrentProject( container );

			ServiceLocator.SetLocatorProvider( () => new UnityServiceLocator( container ) );
			container.RegisterInstance<IServiceLocator>( ServiceLocator.Current );
		}

		private static void RegisterDependenciesInCurrentProject( IUnityContainer container )
		{
			container.RegisterType<IRepository, Repository>();
		}

		private static void RegisterViewModelProviders( IUnityContainer container, Assembly viewModelProvidersAssembly )
		{
			foreach ( var implementation in GetImplementationOfIViewModelProvider( viewModelProvidersAssembly ) )
			{
				var baseInterface = implementation.GetInterfaces().First();
				container.RegisterType( baseInterface, implementation );
			}
		}

		private static IEnumerable<Type> GetImplementationOfIViewModelProvider( Assembly viewModelProvidersAssembly )
		{
			return from t in viewModelProvidersAssembly.GetTypes()
				   from implementedInterface in t.GetInterfaces()
				   where implementedInterface.IsGenericType
				   where implementedInterface.GetGenericTypeDefinition() == typeof( IViewModelProvider<,> )
				   select t;
		}

		private static void RegisterCQRSImplementations( IUnityContainer container )
		{
			container.RegisterType<IEventSubscriberAssembliesProvider, EventSubscriberAssembliesProvider>();
			container.RegisterType<IViewModelToCommandMappingEngine, ViewModelToCommandMappingEngine>();
			container.RegisterType<IDbSessionManager, DbSessionManager>();
		}

		private static void RegisterPseudoCQRSTypes( IUnityContainer container )
		{
			RegisterNonGenericImplementationsWithITypeName( container, typeof( ICommandBus ).Assembly );
			container.RegisterType<IMessageManager, SessionBasedMessageManager>();
			container.RegisterType( typeof( IViewModelFactory<,> ), typeof( ViewModelFactory<,> ) );
		}

		private static void RegisterNonGenericImplementationsWithITypeName( IUnityContainer container, Assembly inAssembly )
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
