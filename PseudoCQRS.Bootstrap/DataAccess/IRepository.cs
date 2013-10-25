using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace PseudoCQRS.Bootstrap.DataAccess
{
	public interface IRepository
	{
		int Save<TEntity>( TEntity entity ) where TEntity : BaseEntity;
		TEntity Get<TEntity>( Expression<Func<TEntity, bool>> expression ) where TEntity : BaseEntity;
		TEntity Get<TEntity>( int id ) where TEntity : BaseEntity;

		List<TEntity> GetAll<TEntity>() where TEntity : BaseEntity;
		List<TEntity> GetAll<TEntity>( Expression<Func<TEntity, bool>> expression ) where TEntity : BaseEntity;
	}
}
