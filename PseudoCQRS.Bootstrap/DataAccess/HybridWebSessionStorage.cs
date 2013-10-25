using System;
using System.Collections.Generic;
using System.Threading;
using System.Web;
using NHibernate;

namespace PseudoCQRS.Bootstrap.DataAccess
{
	public class HybridWebSessionStorage : ISessionStorage
	{
		static ThreadLocal<SimpleSessionStorage> _threadLocalSessionStorage;

		public ISession GetSessionForKey( string factoryKey )
		{
			SimpleSessionStorage storage = GetSimpleSessionStorage();
			return storage.GetSessionForKey( factoryKey );
		}

		public void SetSessionForKey( string factoryKey, ISession session )
		{
			SimpleSessionStorage storage = GetSimpleSessionStorage();
			storage.SetSessionForKey( factoryKey, session );
		}

		public IEnumerable<ISession> GetAllSessions()
		{
			SimpleSessionStorage storage = GetSimpleSessionStorage();
			return storage.GetAllSessions();
		}

		private SimpleSessionStorage GetSimpleSessionStorage()
		{
			HttpContext context = HttpContext.Current;
			SimpleSessionStorage storage;
			if ( context != null )
			{
				storage = context.Items[ HttpContextSessionStorageKey ] as SimpleSessionStorage;
				if ( storage == null )
				{
					storage = new SimpleSessionStorage();
					context.Items[ HttpContextSessionStorageKey ] = storage;
				}
			}
			else
			{
				if ( _threadLocalSessionStorage == null )
					_threadLocalSessionStorage = new ThreadLocal<SimpleSessionStorage>( () => new SimpleSessionStorage() );
				storage = _threadLocalSessionStorage.Value;
			}
			return storage;
		}

		private const string HttpContextSessionStorageKey = "HttpContextSessionStorageKey";

		public string GetCurrentKey()
		{
			return HttpContextSessionStorageKey;
		}

		public void SetCurrentKey( string key )
		{
		}


		public void RemoveCurrentKey()
		{
		}

		public int OpenedTransactions
		{
			get { return GetSimpleSessionStorage().OpenedTransactions; }
			set { GetSimpleSessionStorage().OpenedTransactions = value; }
		}
	}
}
