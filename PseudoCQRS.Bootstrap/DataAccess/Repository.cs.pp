using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using NHibernate.Linq;

namespace PseudoCQRS.Bootstrap.DataAccess
{
	public class Repository : IRepository
	{
		public int Save<TEntity>( TEntity entity ) where TEntity : BaseEntity
		{
			int result;
			var session = FluentSessionFactory.Current;
			if ( entity.Id == 0 )
				result = (int)session.Save( entity );
			else
			{
				session.Update( entity );
				session.Flush();
				result = entity.Id;
			}

			return result;
		}

		public TEntity Get<TEntity>( Expression<Func<TEntity, bool>> expression ) where TEntity : BaseEntity
		{
			TEntity result = FluentSessionFactory.Current.Query<TEntity>().SingleOrDefault( expression );

			return result;
		}

		public TEntity Get<TEntity>( int id ) where TEntity : BaseEntity
		{
			var result = FluentSessionFactory.Current.Get<TEntity>( id );

			return result;
		}

		public List<TEntity> GetAll<TEntity>() where TEntity : BaseEntity
		{
			return GetAll<TEntity>( null );
		}

		public List<TEntity> GetAll<TEntity>( Expression<Func<TEntity, bool>> expression ) where TEntity : BaseEntity
		{
			var query = FluentSessionFactory.Current.Query<TEntity>();
			if ( expression != null )
				query = query.Where( expression );

			List<TEntity> result = query.ToList();

			return result;
		}
	}
}
