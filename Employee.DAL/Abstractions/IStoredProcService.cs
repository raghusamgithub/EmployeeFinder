using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace AWE.Employee.DAL.Abstractions
{
   public interface IStoredProcService
    {
        IList<dynamic> ExecuteStoredProc(string ProcedureName, Dictionary<string, dynamic> Parameter);
        DataTable ExecuteStoredProcDataSet(string sql, Dictionary<string, dynamic> Parameter);
        IList<dynamic> ExecuteRawSQL(string sql);
        int ExecuteRawSQL(string sql, bool isCount = true);
        List<T> ExecuteRawSQL<T>(string sql) where T : class;
        List<T> ExecuteRawSQL<T>(string sql, params object[] parameters) where T : class;

        int ExecuteScalerSQL(string sql);
        List<dynamic> GetMultipleResultSet(string Query, List<Type> collection, params object[] parameters);
        IQueryable<TEntity> EntityFromSql<TEntity>(string sql, params object[] parameters) where TEntity : class;


    }
}
