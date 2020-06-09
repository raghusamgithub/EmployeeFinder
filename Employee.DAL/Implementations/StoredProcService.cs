using AWE.Employee.DAL.Abstractions;
using AWE.Employee.DAL.DBContext;
using AWE.Employee.DAL.QueryResultSet;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWE.Employee.DAL.Implementations
{
    public class StoredProcService : IStoredProcService
    {
        private readonly CommonDBContext _dbContext;

        public StoredProcService(IDbContext context)
        {
            _dbContext = context as CommonDBContext;
        }

        IList<dynamic> IStoredProcService.ExecuteStoredProc(string ProcedureName, Dictionary<string, dynamic> Parameter)
        {
            return _dbContext.ExecuteCommand(ProcedureName, Parameter, true);
        }

        int IStoredProcService.ExecuteRawSQL(string sql, bool isCount)
        {
            return _dbContext.ExecuteCommand(sql, null, false, isCount);
        }

        IList<dynamic> IStoredProcService.ExecuteRawSQL(string sql)
        {
            return _dbContext.ExecuteCommand(sql, null, false);
        }

        public List<T> ExecuteRawSQL<T>(string sql) where T : class
        {
            var list = _dbContext.QueryFromSql<T>(sql);
            return list.ToList();
        }

        public List<T> ExecuteRawSQL<T>(string sql, params object[] parameters) where T : class
        {
            var list = _dbContext.QueryFromSql<T>(sql, parameters);
            return list.ToList();
        }

        DataTable IStoredProcService.ExecuteStoredProcDataSet(string ProcedureName, Dictionary<string, dynamic> Parameter)
        {
            return _dbContext.ExecuteCommandDataSet(ProcedureName, Parameter, true);
        }

        public IQueryable<TEntity> EntityFromSql<TEntity>(string sql, params object[] parameters) where TEntity : class
        {
            return _dbContext.EntityFromSql<TEntity>(sql, parameters);
        }

        public List<dynamic> GetMultipleResultSet(string Query, List<Type> collection, params object[] parameters)
        {
            return _dbContext.GetMultipleResultSet(Query, collection, parameters);
        }

        public int ExecuteScalerSQL(string sql)
        {
            return this.ExecuteRawSQL<ScalarValue>(sql).FirstOrDefault()?.Value ?? 0;
        }
    }
}
