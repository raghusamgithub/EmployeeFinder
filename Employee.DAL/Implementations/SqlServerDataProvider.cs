using System.Data.Common;
using AWE.Employee.DAL.Abstractions;
using Microsoft.Data.SqlClient;

namespace AWE.Employee.DAL.Implementations
{
    public class SqlServerDataProvider : IDataProvider
    {
        public virtual DbParameter GetParameter()
        {
            return new SqlParameter();
        }
    }
}
