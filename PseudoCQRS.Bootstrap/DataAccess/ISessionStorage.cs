using System.Collections.Generic;
using NHibernate;

namespace PseudoCQRS.Bootstrap.DataAccess
{
	public interface ISessionStorage
	{
		IEnumerable<ISession> GetAllSessions();
		ISession GetSessionForKey( string factoryKey );
		void SetSessionForKey( string factoryKey, ISession session );
		string GetCurrentKey();
		void SetCurrentKey( string key );
		void RemoveCurrentKey();
		int OpenedTransactions { get; set; }
	}
}
