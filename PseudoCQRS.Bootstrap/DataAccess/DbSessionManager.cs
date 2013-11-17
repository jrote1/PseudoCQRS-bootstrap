using System;
using System.Data;

namespace PseudoCQRS.Bootstrap.DataAccess
{
	internal class DbSessionManager : IDbSessionManager
	{
		public void CloseSession()
		{
			FluentSessionFactory.CloseCurrentSession();
		}

		public void OpenTransaction()
		{
			FluentSessionFactory.IncreaseOpenedTransactionsValue();
			if ( !FluentSessionFactory.Current.Transaction.IsActive )
				FluentSessionFactory.Current.BeginTransaction( IsolationLevel.ReadCommitted );
		}

		public void CommitTransaction()
		{
			FluentSessionFactory.DecreaseOpenedTransactionsValue();
			if ( FluentSessionFactory.Current.Transaction.IsActive && FluentSessionFactory.GetOpenedTransactionsValue() == 0 )
				FluentSessionFactory.Current.Transaction.Commit();
		}

		public void RollbackTransaction()
		{
			if ( FluentSessionFactory.Current.Transaction.IsActive && FluentSessionFactory.GetOpenedTransactionsValue() == 0 )
				FluentSessionFactory.Current.Transaction.Rollback();
		}
	}
}
