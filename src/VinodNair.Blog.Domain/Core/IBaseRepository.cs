using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace VinodNair.Blog.Domain.Core
{
    public interface IBaseRepository<T> where T : class
    {
        /// <summary>
        /// Function use to get Object flow Id
        /// </summary>
        /// <param name="id">Primary key of Table current</param>
        /// <returns></returns>
        Task<T> GetByIdAsync(int id);

        ///// <summary>
        ///// Get All list Object
        ///// </summary>
        ///// <returns></returns>
        IQueryable<T> GetQueryable();



        ///// <summary>
        ///// Function use in the case Query have condition
        ///// </summary>
        ///// <param name="filter">Condition of query</param>
        ///// <returns></returns>
        IQueryable<T> Query(Expression<Func<T, bool>> filter);

        /// <summary>
        /// Function use to Update Object 
        /// </summary>
        /// <param name="entity">Object is targer Update</param>
        /// <returns></returns>
        Task<T> UpdateAsync(T entity);

        /// <summary>
        /// Function use to Insert Object 
        /// </summary>
        /// <param name="entity">Object is targer Update</param>
        /// <returns></returns>
        Task<T> InsertAsync(T entity);

        /// <summary>
        /// Inserts the multiple entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        Task<List<T>> InsertMultiAsync(List<T> entity);

        /// <summary>
        /// Function use to Remove Object in Database
        /// </summary>
        /// <param name="entity">Object is targer Update</param>
        /// <returns></returns>
        Task<bool> DeleteAsync(T entity);

        /// <summary>
        /// Function use to Remove Object in Database
        /// </summary>
        /// <param name="id">Id is identity</param>
        /// <returns></returns>
        Task<bool> DeleteAsync(dynamic id);

        /// <summary>
        /// Deletes the mullti.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        Task<bool> DeleteMultiAsync(List<T> entity);

        Task<T> FindAsync(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes);
        Task<List<T>> FindAllAsync(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes);
    }
}
