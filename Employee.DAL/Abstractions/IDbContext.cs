using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace AWE.Employee.DAL.Abstractions
{
    public partial interface IDbContext
    {
        #region Methods

        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        int SaveChanges();

        IQueryable<TQuery> QueryFromSql<TQuery>(string sql) where TQuery : class;

        IQueryable<TQuery> QueryFromSql<TQuery>(string sql,params object[] parameters) where TQuery : class;

        IQueryable<TEntity> EntityFromSql<TEntity>(string sql, params object[] parameters) where TEntity : class;


        int ExecuteSqlCommand(string sql, bool doNotEnsureTransaction = false, int? timeout = null, params object[] parameters);

        void Detach<TEntity>(TEntity entity) where TEntity : class;

        List<dynamic> GetMultipleResultSet(string Query, List<Type> collection, params object[] parameters);
        #endregion
    }
}
