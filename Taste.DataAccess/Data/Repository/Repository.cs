using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Taste.DataAccess.Data.Repository.IRepository;

namespace Taste.DataAccess.Data.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected internal DbContext _dbcontext;
        internal DbSet<T> _dbset;
        public Repository(DbContext dbContext)
        {
            _dbcontext = dbContext;
            _dbset = _dbcontext.Set<T>();
        }
        public void Add(T entity)
        {
            _dbset.Add(entity);
        }
        public T Get(int id)
        {
            return _dbset.Find(id);
        }
        public IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includedProperties = null)
        {
            IQueryable<T> query = _dbset;
            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includedProperties != null)
            {
                foreach (var includeProperty in includedProperties.Split(new char[]{','}, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            if(orderBy != null)
            {
                return orderBy(query).ToList();
            }

            return query.ToList();
        }
        public T GetFirstOrDefault(Expression<Func<T, bool>> filter = null, string includedProperties = null)
        {
            IQueryable<T> query = _dbset;
            if(filter != null)
            {
                query = query.Where(filter);
            }

            if(includedProperties != null)
            {
                foreach (var includeProperty in includedProperties.Split(new char[] { ','}, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            return query.FirstOrDefault();
        }
        public void Remove(int id)
        {
            T findEntity = _dbset.Find(id);
            if (findEntity != null)
            {
                Remove(findEntity);
            }
        }
        public void Remove(T entity)
        {
            _dbset.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entity)
        {
            _dbcontext.RemoveRange(entity);
        }
    }
}
