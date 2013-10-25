using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.Practices.Unity;

namespace $rootnamespace$
{
	public class Bootstrapper
	{
		public void Bootstrap( IUnityContainer container, Assembly viewModelProvidersAssembly )
		{
			AutoMapperInitializer.Initialise();
			UnityRegistrar.Register( container, viewModelProvidersAssembly );
		}
	}
}
