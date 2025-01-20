
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using VinodNair.Blog.Application.Contracts.Core;
using VinodNair.Blog.Domain.Core;

namespace VinodNair.Blog.Application.Services.Core
{
    public class EntityService<T> : IEntityService<T> where T : class, new()
    {
        protected readonly IUnitOfWork UnitOfWork;
        readonly IBaseRepository<T> _repository;
        //protected static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        protected EntityService(IUnitOfWork unitOfWork, IBaseRepository<T> repository)
        {
            UnitOfWork = unitOfWork;
            _repository = repository;
        }

        public async Task<T> InsertAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            await _repository.InsertAsync(entity);
            UnitOfWork.SaveChanges();

            return entity;
        }

        public async Task<List<T>> InsertMultiAsync(List<T> entity)
        {
            try
            {
                await _repository.InsertMultiAsync(entity);
                UnitOfWork.SaveChanges();

                return entity;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> DeleteAsync(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException("entity");

                await _repository.DeleteAsync(entity);
                UnitOfWork.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(dynamic id)
        {
            try
            {
                var isDeleted = await _repository.DeleteAsync(id);
                UnitOfWork.SaveChanges();
                return isDeleted;
            }
            catch (Exception ex)
            {

                return false;
            }

        }

        public async Task<bool> DeleteMultiAsync(List<T> entity)
        {
            try
            {
                await _repository.DeleteMultiAsync(entity);
                UnitOfWork.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }
        public async Task<T> FindAsync(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes)
        {
            return await _repository.FindAsync(expression, includes);
        }
        public async Task<List<T>> FindAllAsync(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes)
        {
            return await _repository.FindAllAsync(expression, includes);
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return _repository.GetQueryable();
        }

        public async Task<T> UpdateAsync(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            await _repository.UpdateAsync(entity);
            UnitOfWork.SaveChanges();

            return entity;
        }



        public IQueryable<T> GetQueryable()
        {
            return _repository.GetQueryable();
        }
    }
}
