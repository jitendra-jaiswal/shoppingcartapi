using System.Linq.Expressions;

namespace ShoppingCart.Infrastructure
{
    public interface IRepository<TEntity>
    {

        List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null, List<Expression<Func<TEntity, object>>> includes = null);

        TEntity GetFirstOrDefault(Expression<Func<TEntity, bool>> filter = null);

        int Insert(TEntity entity);

        int Update(TEntity entity);

        int Remove(TEntity entity);
        int Remove(object id);

        int Save();

    }

}