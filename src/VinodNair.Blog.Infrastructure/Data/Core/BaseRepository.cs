using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using VinodNair.Blog.Domain.Core;
using VinodNair.Blog.Infrastructure.Data.Contexts;

namespace VinodNair.Blog.Infrastructure.Data.Core
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected DbSet<T> Dbset;
        protected BlogDbContext DbContext;

        public BaseRepository(BlogDbContext context)
        {
            DbContext = context;
            Dbset = context.Set<T>();
        }

        public async Task<bool> DeleteAsync(T entity)
        {
            try
            {
                var entry = DbContext.Entry(entity);
                entry.State = EntityState.Deleted;
                DbContext.SaveChanges();
            }
            catch (Exception)
            {
                await Task.FromResult(false);
            }
            return await Task.FromResult(true); ;
        }

        public async Task<bool> DeleteAsync(dynamic id)
        {
            var entity = await GetByIdAsync(id);
            if (entity == null)
            {
                return false;
            }

            await DeleteAsync(entity);
            return true;
        }

        public async Task<bool> DeleteMultiAsync(List<T> entity)
        {
            try
            {
                foreach (var item in entity)
                {
                    await DeleteAsync(item);
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }


        public async Task<T> FindAsync(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes)
        {
            var query = Dbset.AsQueryable();

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }
            query = query.Where(expression);
            return await query?.FirstOrDefaultAsync();
        }

        public async Task<List<T>> FindAllAsync(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes)
        {
            var query = Dbset.AsQueryable();
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }
            if (expression != null)
            {
                query = query.Where(expression);
            }
            return await query.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await Dbset.FindAsync(id);

        }

        public IQueryable<T> GetQueryable()
        {
            return Dbset.AsQueryable();
        }

        public async Task<T> InsertAsync(T entity)
        {
            try
            {
                Dbset.Add(entity);
                return await Task.FromResult(entity);
            }
            catch (Exception)
            {

                return null;
            }

        }

        public async Task<List<T>> InsertMultiAsync(List<T> entity)
        {
            try
            {
                Dbset.AddRange(entity);
                return await Task.FromResult(entity);
            }
            catch (Exception)
            {
                return null;
            }

        }

        public IQueryable<T> Query(Expression<Func<T, bool>> filter)
        {
            return Dbset.Where(filter);
        }

        public async Task<T> UpdateAsync(T entity)
        {
            try
            {
                var dbEntityEntry = DbContext.Entry(entity);
                if (dbEntityEntry.State == EntityState.Detached)
                {
                    Dbset.Attach(entity);
                }

                dbEntityEntry.State = EntityState.Modified;
                return await Task.FromResult(entity);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
