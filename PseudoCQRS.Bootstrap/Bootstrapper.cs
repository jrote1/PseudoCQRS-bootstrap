using System.Reflection;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using PseudoCQRS.Bootstrap.DataAccess;

namespace PseudoCQRS.Bootstrap
{
	public class Bootstrapper
	{
		/* RFAD: use a fluent builder to set these settings. refactored should be able to select one assembly if using only one project or set different assemblies. */
		public static void Bootstrap(
			IUnityContainer container,
			Assembly autoMapperProfilesAssembly,
			Assembly viewModelProvidersAssembly,
			string connectionString,
			Assembly entitiesAssembly,
			Assembly fluentMappingsAssembly,
			Assembly mappingsOverridesAssembly )
		{
			// should we install fluent validation ??
			AutoMapperInitializer.Initialise( autoMapperProfilesAssembly );
			UnityRegistrar.Register( container, viewModelProvidersAssembly );
			ControllerBuilder.Current.SetControllerFactory( new UnityControllerFactory( container ) );

			FluentSessionFactory.InitialiseSessionFactory( connectionString, false, new HybridWebSessionStorage(), entitiesAssembly, fluentMappingsAssembly, mappingsOverridesAssembly );
		}
	}
}
