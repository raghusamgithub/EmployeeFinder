using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AWE.Employee.DAL.Abstractions
{
    public partial interface IRepository<TEntity> where TEntity : class
    {
        #region Methods
        /// <summary>
        /// Get Entity By Identifier
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TEntity GetById(object id);

        /// <summary>
        /// Insert Entity
        /// </summary>
        /// <param name="entity"></param>
        void Insert(TEntity entity);

        /// <summary>
        /// Insert Entities
        /// </summary>
        /// <param name="entities"></param>
        void Insert(IEnumerable<TEntity> entities);

        /// <summary>
        /// Update Entity
        /// </summary>
        /// <param name="entity"></param>
        void Update(TEntity entity);

        /// <summary>
        /// Update Entities
        /// </summary>
        /// <param name="entities"></param>
        void Update(IEnumerable<TEntity> entities);

        /// <summary>
        /// Delete Entity
        /// </summary>
        /// <param name="entity"></param>
        void Delete(TEntity entity);

        /// <summary>
        /// Deelte Entities
        /// </summary>
        /// <param name="entities"></param>
        void Delete(IEnumerable<TEntity> entities);
        #endregion

        #region Properties

        /// <summary>
        /// Gets a Table
        /// </summary>
        IQueryable<TEntity> Table { get; }

        /// <summary>
        /// Gets a table with  "No tacking enable (EF Feature) User it only when you load record(s) only for read-only operations"
        /// </summary>
        IQueryable<TEntity> TableNoTracking { get; }

        #endregion
    }
}
