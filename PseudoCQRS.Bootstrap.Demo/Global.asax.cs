using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using FluentValidation.Mvc;
using Microsoft.Practices.Unity;

namespace PseudoCQRS.Bootstrap.Demo
{
	// Note: For instructions on enabling IIS6 or IIS7 classic mode, 
	// visit http://go.microsoft.com/?LinkId=9394801
	public class MvcApplication : HttpApplication
	{
		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();

			WebApiConfig.Register( GlobalConfiguration.Configuration );
			FilterConfig.RegisterGlobalFilters( GlobalFilters.Filters );
			RouteConfig.RegisterRoutes( RouteTable.Routes );

			string connecitonString = ConfigurationManager.ConnectionStrings[ "dev" ].ConnectionString;
			IUnityContainer container = new UnityContainer();
			var webApplicationAssembly = Assembly.GetExecutingAssembly();
			Bootstrapper.Bootstrap( container, webApplicationAssembly, webApplicationAssembly, connecitonString, this, webApplicationAssembly, webApplicationAssembly, webApplicationAssembly );
			FluentValidationModelValidatorProvider.Configure();
		}
	}
}