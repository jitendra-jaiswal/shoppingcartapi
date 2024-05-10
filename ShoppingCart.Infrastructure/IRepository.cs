using System.Linq.Expressions;

namespace ShoppingCart.Infrastructure
{
    public interface IRepository<TEntity>
    {

        List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null, List<Expression<Func<TEntity, object>>> includes = null);

        TEntity GetFirstOrDefault(Expression<Func<TEntity, bool>> filter = null);

        Task<int> Insert(TEntity entity);

        Task<int> Update(TEntity entity);

        Task<int> Remove(TEntity entity);
        Task<int> Remove(object id);

        Task<int> Save();

    }

}