﻿using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ShoppingCart.Infrastructure
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly EcommerceContext _context;
        private DbSet<TEntity> _dbSet;
        public Repository(EcommerceContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }
        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null, List<Expression<Func<TEntity, object>>> includes = null)
        {
            IQueryable<TEntity> query = _dbSet;

            if (filter != null)
                query = query.Where(filter);

            if (includes != null)
            {
                includes.ForEach(x => query.Include(x).Load());
            }

            return query.ToList();
        }

        public TEntity GetFirstOrDefault(Expression<Func<TEntity, bool>> filter = null)
        {
            IQueryable<TEntity> query = _dbSet;
            return query.FirstOrDefault(filter);
        }

        public async Task<int> Insert(TEntity entity)
        {
            _dbSet.Add(entity);
            return await Save();

        }

        public async Task<int> Remove(TEntity entity)
        {
            _dbSet.Remove(entity);
            return await Save();
        }

        public async Task<int> Remove(object id)
        {
            TEntity entity = _dbSet.Find(id);
            if (entity == null)
                return 0;
            _dbSet.Remove(entity);
            return await Save();
        }

        public async Task<int> Save()
        {
            return await Task.FromResult(_context.SaveChanges(true));
        }

        public async Task<int> Update(TEntity entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            return await Save();
        }
    }
}
