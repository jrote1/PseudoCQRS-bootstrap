using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Reflection;
using System.Text;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Conventions.Helpers;
using NHibernate.Cfg;

namespace PseudoCQRS.Bootstrap.DataAccess
{
	public static class FluentNHibernateMappingsInitializer
	{
		public static Configuration BuildConfiguration( string connectionString, bool showSql, Assembly entitiesAssembly, Assembly fluentMappingsAssembly, Assembly mappingsOverridesAssembly )
		{
			MsSqlConfiguration cfg = MsSqlConfiguration.MsSql2008.ConnectionString( connectionString );

			if ( showSql )
				cfg = cfg.ShowSql().FormatSql();


			return Fluently.Configure()
						   .Database( cfg )
						   .Mappings( x =>
						   {
							   x.FluentMappings.AddFromAssembly( fluentMappingsAssembly );
							   x.AutoMappings.Add( GetAutoMappingSettings(entitiesAssembly, mappingsOverridesAssembly) );
						   } )
						   .BuildConfiguration();
			
		}

		private static AutoPersistenceModel GetAutoMappingSettings( Assembly entitiesAssembly, Assembly mappingsOverridesAssembly )
		{
			var persistenceModel = new AutoPersistenceModel();
			persistenceModel.AddEntityAssembly( entitiesAssembly ).Where( x => x.IsSubclassOf( typeof ( BaseEntity ) ) );
			persistenceModel.UseOverridesFromAssembly( mappingsOverridesAssembly );
			persistenceModel.Conventions.Add(				
				PrimaryKey.Name.Is( x => "Id" ),
				ConventionBuilder.Id.Always( x => x.GeneratedBy.Identity() ),
				ConventionBuilder.Property.Always( x => x.Not.Nullable() ),
				ConventionBuilder.Property.When( expectation =>
					expectation.Expect( propertyInspector => propertyInspector.Property.PropertyType == typeof( string ) ),
					instance => instance.Length( 256 ) ),
				ConventionBuilder.Reference.Always( x => x.Not.Nullable() ),
				ForeignKey.EndsWith( "Id" )
			);

			return persistenceModel;
		}

	}
}
