using System.Reflection;
using NHibernate;
using NHibernate.Cfg;

namespace PseudoCQRS.Bootstrap.DataAccess
{
	public static class FluentSessionFactory
	{

		private static ISessionFactory _sessionFactory;

		private static ISessionStorage Storage { get; set; }

		private readonly static object LockObject = new object();


		public static ISession Current
		{
			get
			{
				var key = Storage.GetCurrentKey();
				var session = Storage.GetSessionForKey( key );
				if ( session == null )
				{
					lock ( LockObject )
					{
						session = Storage.GetSessionForKey( key );
						if ( session == null )
						{
							session = _sessionFactory.OpenSession();
							Storage.SetSessionForKey( key, session );
						}
					}

				}
				return session;
			}
		}

		public static Configuration NHibernateConfiguration { get; private set; }


		public static void InitialiseSessionFactory( string connectionString, bool showSql, ISessionStorage sessionStorage, Assembly entitiesAssembly, Assembly fluentMappingsAssembly, Assembly mappingsOverridesAssembly )
		{
			NHibernateConfiguration = FluentNHibernateMappingsInitializer.BuildConfiguration( connectionString, showSql, entitiesAssembly, fluentMappingsAssembly, mappingsOverridesAssembly );
			_sessionFactory = NHibernateConfiguration.BuildSessionFactory();
			Storage = sessionStorage;
		}


		public static void CloseCurrentSession()
		{
			var key = Storage.GetCurrentKey();
			var session = Storage.GetSessionForKey( key );

			if ( session == null )
				return;

			if ( session.IsOpen )
			{

				session.Close();
				Storage.RemoveCurrentKey();
			}
			session.Dispose();
		}

		public static void IncreaseOpenedTransactionsValue()
		{
			Storage.OpenedTransactions++;
		}

		public static void DecreaseOpenedTransactionsValue()
		{
			Storage.OpenedTransactions--;
		}

		public static int GetOpenedTransactionsValue()
		{
			return Storage.OpenedTransactions;
		}
	}
}
