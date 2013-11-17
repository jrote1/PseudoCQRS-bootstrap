using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentValidation.Mvc;
using Microsoft.Practices.Unity;

namespace PseudoCQRS.Bootstrap
{
	public static class Bootstrapper
	{
		public static void Initialize()
		{
			IUnityContainer container = new UnityContainer();
			const string connectionString = "";
			var mainAssembly = typeof( Bootstrapper ).Assembly;
			PseudoCQRSBootstrapper.Bootstrap( container, connectionString, mainAssembly );
			UnityRegistrar.Register( container, mainAssembly );
			FluentValidationModelValidatorProvider.Configure();
		}
	}
}
