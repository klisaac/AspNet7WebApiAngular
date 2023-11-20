
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AspNet7.Core.Pagination;
using AspNet7.Core.Specifications;
using AspNet7.Core.Entities.Base;
using AspNet7.Core.Repository.Base;
using AspNet7.Core.Specifications.Base;
using AspNet7.Infrastructure.Data;

namespace AspNet7.Infrastructure.Repository.Base
{
    public class BaseRepository<T> : IBaseRepository<T> where T : AuditEntity
    {
        protected readonly AspNet7DataContext _aspNet5DataContext;
        private DbSet<T> _entities;

        public BaseRepository()
        {
        }
        public BaseRepository(AspNet7DataContext aspNet5DataContext)
        {
            _aspNet5DataContext = aspNet5DataContext ?? throw new ArgumentNullException(nameof(aspNet5DataContext));
        }

        protected virtual DbSet<T> Entities
        {
            get
            {
                if (_entities == null)
                    _entities = _aspNet5DataContext.Set<T>();

                return _entities;
            }
        }

        public IQueryable<T> Table => Entities.Where(t => t.IsDeleted == false);

        public IQueryable<T> TableNoTracking => Entities.Where(t => t.IsDeleted == false).AsNoTracking();

        public async virtual Task<IReadOnlyList<T>> ListAllAsync()
        {
            return await Entities.Where(t => t.IsDeleted == false).ToListAsync();
        }
       
        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _aspNet5DataContext.Set<T>().Where(t => t.IsDeleted == false).ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAllByIncludingAsync(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _aspNet5DataContext.Set<T>();
            foreach (var includeProperty in includeProperties)
                query = query.Include(includeProperty);

            return await query.Where(c => c.IsDeleted == false).ToListAsync();
        }
        

        public async Task<IReadOnlyList<T>> GetAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }

        public async Task<int> CountAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).CountAsync();
        }

        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(_aspNet5DataContext.Set<T>().Where(t => t.IsDeleted == false).AsQueryable(), spec);
        }

        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return await _aspNet5DataContext.Set<T>().Where(predicate.And(t => t.IsDeleted==false)).ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeString = null, bool disableTracking = true)
        {
            IQueryable<T> query = _aspNet5DataContext.Set<T>().Where(t => t.IsDeleted == false);
            if (disableTracking) query = query.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(includeString)) query = query.Include(includeString);

            if (predicate != null) query = query.Where(predicate);

            if (orderBy != null)
                return await orderBy(query).ToListAsync();
            return await query.ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, List<Expression<Func<T, object>>> includes = null, bool disableTracking = true)
        {
            IQueryable<T> query = _aspNet5DataContext.Set<T>().Where(t => t.IsDeleted == false);
            if (disableTracking) query = query.AsNoTracking();

            if (includes != null) query = includes.Aggregate(query, (current, include) => current.Include(include));

            if (predicate != null) query = query.Where(predicate);

            if (orderBy != null)
                return await orderBy(query).ToListAsync();
            return await query.ToListAsync();
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await _aspNet5DataContext.Set<T>().FindAsync(id);
        }

        public async Task<T> GetSingleAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities)
        {
            _aspNet5DataContext.Entry(entities).State = EntityState.Added;
            await Entities.AddRangeAsync(entities);
            await _aspNet5DataContext.SaveChangesAsync();
            return entities;
        }

        public async Task<T> AddAsync(T entity)
        {
            _aspNet5DataContext.ChangeTracker.LazyLoadingEnabled = true;
            _aspNet5DataContext.Entry(entity).State = EntityState.Added;
            await _aspNet5DataContext.Set<T>().AddAsync(entity);
            await _aspNet5DataContext.SaveChangesAsync();
            return entity;
        }

        public async Task<T> AddByLoadingReferenceAsync(T entity, params Expression<Func<T, object>>[] loadProperties)
        {
            _aspNet5DataContext.Entry(entity).State = EntityState.Added;
            foreach (var loadProperty in loadProperties)
                _aspNet5DataContext.Entry(entity).Reference(loadProperty).Load();

            await _aspNet5DataContext.Set<T>().AddAsync(entity);
            await _aspNet5DataContext.SaveChangesAsync();
            return entity;
        }

        public async Task<T> AddByLoadingCollectionAsync(T entity, params Expression<Func<T, IEnumerable<object>>>[] loadProperties)
        {
            _aspNet5DataContext.Entry(entity).State = EntityState.Added;
            foreach (var loadProperty in loadProperties)
                _aspNet5DataContext.Entry(entity).Collection(loadProperty).Load();

            await _aspNet5DataContext.Set<T>().AddAsync(entity);
            await _aspNet5DataContext.SaveChangesAsync();
            return entity;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            //_dbContext.Entry(entity).State = EntityState.Detached;
            _aspNet5DataContext.Entry(entity).State = EntityState.Modified;
            await _aspNet5DataContext.SaveChangesAsync();
            return await Task.FromResult(entity);
        }

        public async Task<T> UpdateByLoadingReferenceAsync(T entity, params Expression<Func<T, object>>[] loadProperties)
        {
            _aspNet5DataContext.Entry(entity).State = EntityState.Modified;
            foreach (var loadProperty in loadProperties)
                _aspNet5DataContext.Entry(entity).Reference(loadProperty).Load();

            await _aspNet5DataContext.SaveChangesAsync();
            return await Task.FromResult(entity);
        }

        public async Task DeleteAsync(T entity)
        {
            _aspNet5DataContext.Set<T>().Remove(entity);
            await _aspNet5DataContext.SaveChangesAsync();
        }

        //public async Task SaveChangesAsync()
        //{
        //    await _aspNet5DataContext.SaveChangesAsync();
        //}
    }
}
