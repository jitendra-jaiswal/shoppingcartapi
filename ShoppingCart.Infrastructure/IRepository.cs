using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Infrastructure
{
    public interface IRepository<TEntity>
    {

        List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null);

        TEntity GetFirstOrDefault(Expression<Func<TEntity, bool>> filter = null);

        int Insert(TEntity entity);

        int Update(TEntity entity);

        int Remove(TEntity entity);
        int Remove(object id);

        int Save();

    }

}