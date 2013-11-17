using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Practices.Unity;
using PseudoCQRS.Bootstrap.DataAccess;
using PseudoCQRS.Controllers;

namespace PseudoCQRS.Bootstrap
{
	internal static class UnityCQRSRegistrations
	{
		public static void Register( IUnityContainer container, Assembly viewModelProvidersAssembly )
		{
			RegisterPseudoCQRSTypes( container );
			RegisterViewModelProviders( container, viewModelProvidersAssembly );
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

		private static void RegisterPseudoCQRSTypes( IUnityContainer container )
		{
			container.RegisterNonGenericImplementationsWithITypeName( typeof( ICommandBus ).Assembly );
			container.RegisterType<IDbSessionManager, DbSessionManager>();
			container.RegisterType<IViewModelToCommandMappingEngine, ViewModelToCommandMappingEngine>();
			container.RegisterType<IMessageManager, SessionBasedMessageManager>();
			container.RegisterType( typeof( IViewModelFactory<,> ), typeof( ViewModelFactory<,> ) );
		}
	}
}
