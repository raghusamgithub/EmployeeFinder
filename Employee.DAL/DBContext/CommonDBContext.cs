using AWE.Employee.DAL.Abstractions;
using AWE.Employee.DAL.Mapping;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AWE.Employee.DAL.DBContext
{
    public partial class CommonDBContext : DbContext, IDbContext
    {

        #region Ctor
        // public CommonDBContext() { }

        public CommonDBContext(DbContextOptions<CommonDBContext> options) : base(options)
        {
        }

        #endregion

        #region Utilities

        /// <summary>
        /// Further configuration the model
        /// </summary>
        /// <param name="modelBuilder">The builder being used to construct the model for this context</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //dynamically load all entity and query type configurations
            var typeConfigurations = Assembly.GetExecutingAssembly().GetTypes().Where(type =>
                (type.BaseType?.IsGenericType ?? false)
                    && (type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>)));

            foreach (var typeConfiguration in typeConfigurations)
            {
                var configuration = (IMappingConfiguration)Activator.CreateInstance(typeConfiguration);
                configuration.ApplyConfiguration(modelBuilder);
            }

            typeConfigurations = Assembly.GetExecutingAssembly().GetTypes().Where(type =>
                (type.BaseType?.IsGenericType ?? false)
                    && (type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>)));

            foreach (var typeConfiguration in typeConfigurations)
            {
                var configuration = (IMappingConfiguration)Activator.CreateInstance(typeConfiguration);
                configuration.ApplyConfiguration(modelBuilder);
            }
            base.OnModelCreating(modelBuilder);

        }


        #endregion

        #region Methods

        /// <summary>
        /// Creates a DbSet that can be used to query and save instances of entity
        /// </summary>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <returns>A set for the given entity type</returns>
        public virtual new DbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }



        /// <summary>
        /// Creates a LINQ query for the query type based on a raw SQL query
        /// </summary>
        /// <typeparam name="TQuery">Query type</typeparam>
        /// <param name="sql">The raw SQL query</param>
        /// <returns>An IQueryable representing the raw SQL query</returns>
        public virtual IQueryable<TQuery> QueryFromSql<TQuery>(string sql) where TQuery : class
        {
            return this.Set<TQuery>().FromSqlRaw(sql);
        }

        public virtual IQueryable<TQuery> QueryFromSql<TQuery>(string sql, params object[] parameters) where TQuery : class
        {
            return this.Set<TQuery>().FromSqlRaw(sql, parameters);
        }

        /// <summary>
        /// Creates a LINQ query for the entity based on a raw SQL query
        /// </summary>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <param name="sql">The raw SQL query</param>
        /// <param name="parameters">The values to be assigned to parameters</param>
        /// <returns>An IQueryable representing the raw SQL query</returns>
        public virtual IQueryable<TEntity> EntityFromSql<TEntity>(string sql, params object[] parameters) where TEntity : class
        {
            if (parameters == null)
            {
                return this.Set<TEntity>().FromSqlRaw(sql);
            }
            return this.Set<TEntity>().FromSqlRaw(sql, parameters);

        }

        /// <summary>
        /// Executes the given SQL against the database
        /// </summary>
        /// <param name="sql">The SQL to execute</param>
        /// <param name="doNotEnsureTransaction">true - the transaction creation is not ensured; false - the transaction creation is ensured.</param>
        /// <param name="timeout">The timeout to use for command. Note that the command timeout is distinct from the connection timeout, which is commonly set on the database connection string</param>
        /// <param name="parameters">Parameters to use with the SQL</param>
        /// <returns>The number of rows affected</returns>
        public virtual int ExecuteSqlCommand(string sql, bool doNotEnsureTransaction = false, int? timeout = null, params object[] parameters)
        {
            //set specific command timeout
            var previousTimeout = this.Database.GetCommandTimeout();
            this.Database.SetCommandTimeout(timeout);

            var result = 0;
            if (!doNotEnsureTransaction)
            {
                //use with transaction
                using (var transaction = this.Database.BeginTransaction())
                {
                    result = this.Database.ExecuteSqlRaw(sql, parameters);
                    transaction.Commit();
                }
            }
            else
                result = this.Database.ExecuteSqlRaw(sql, parameters);

            //return previous timeout back
            this.Database.SetCommandTimeout(previousTimeout);

            return result;
        }

        public IList<dynamic> ExecuteCommand(string ProcedureName, Dictionary<string, dynamic> Parameter, bool isProcedure)
        {
            string Params = " ";
            var parameters = new List<SqlParameter>();
            char[] trimChar = new char[] { '@', ',' };
            if (Parameter != null)
            {
                foreach (var item in Parameter.Keys)
                {
                    Params = Params + "@" + item.Trim(trimChar) + ", ";
                    parameters.Add(new SqlParameter("@" + item.Trim(trimChar), Parameter[item]));
                }
            }
            Params = Params.Trim(trimChar);
            Params = Params.TrimEnd(new char[] { ',', ' ' });

            List<dynamic> dataSet = new List<dynamic>();
            TextInfo ti = CultureInfo.CurrentCulture.TextInfo;
            using (var command = this.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = ProcedureName;
                command.Parameters.AddRange(parameters.ToArray());
                command.CommandType = isProcedure ? CommandType.StoredProcedure : CommandType.Text;
                this.Database.OpenConnection();
                using (var result = command.ExecuteReader())
                {
                    do
                    {
                        List<dynamic> dataTable = new List<dynamic>();
                        var names = Enumerable.Range(0, result.FieldCount).Select(result.GetName).ToList();
                        foreach (IDataRecord record in result as IEnumerable)
                        {
                            var expando = new ExpandoObject() as IDictionary<string, object>;
                            foreach (var name in names)
                                expando[ti.ToTitleCase(name.ToLower().Replace("_", " ").Replace("-", " ")).Replace(" ", "")] = record[name];
                            dataTable.Add(expando);
                        }
                        dataSet.Add(dataTable);
                    } while (result.NextResult());
                }
            }
            return dataSet;
        }

        public int ExecuteCommand(string ProcedureName, Dictionary<string, dynamic> Parameter, bool isProcedure, bool isCount)
        {
            string Params = " ";
            var parameters = new List<SqlParameter>();
            char[] trimChar = new char[] { '@', ',' };
            if (Parameter != null)
            {
                foreach (var item in Parameter.Keys)
                {
                    Params = Params + "@" + item.Trim(trimChar) + ", ";
                    parameters.Add(new SqlParameter("@" + item.Trim(trimChar), Parameter[item]));
                }
            }
            Params = Params.Trim(trimChar);
            Params = Params.TrimEnd(new char[] { ',', ' ' });

            List<dynamic> dataSet = new List<dynamic>();
            int result;
            TextInfo ti = CultureInfo.CurrentCulture.TextInfo;
            using (var command = this.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = ProcedureName;
                command.Parameters.AddRange(parameters.ToArray());
                command.CommandType = isProcedure ? CommandType.StoredProcedure : CommandType.Text;

                this.Database.OpenConnection();
                result = Convert.ToInt32(command.ExecuteScalar().ToString());
            }
            return result;
        }


        public DataTable ExecuteCommandDataSet(string ProcedureName, Dictionary<string, dynamic> Parameter, bool isProcedure)
        {
            string Params = " ";
            var parameters = new List<SqlParameter>();
            char[] trimChar = new char[] { '@', ',' };
            if (Parameter != null)
            {
                foreach (var item in Parameter.Keys)
                {
                    Params = Params + "@" + item.Trim(trimChar) + ", ";
                    parameters.Add(new SqlParameter("@" + item.Trim(trimChar), Parameter[item]));
                }
            }
            Params = Params.Trim(trimChar);
            Params = Params.TrimEnd(new char[] { ',', ' ' });

            DataSet dataSet = new DataSet();
            int i = 0;
            using (var command = this.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = ProcedureName;
                command.Parameters.AddRange(parameters.ToArray());
                command.CommandType = isProcedure ? CommandType.StoredProcedure : CommandType.Text;
                this.Database.OpenConnection();
                using (var result = command.ExecuteReader())
                {
                    do
                    {
                        DataTable dt = new DataTable
                        {
                            TableName = "DB_" + (++i).ToString("00")
                        };
                        dt.Load(result);
                        dataSet.Tables.Add(dt);
                    } while (!result.IsClosed && result.NextResult());
                }
            }
            return dataSet.Tables[0];
        }

        /// <summary>
        /// Detach an entity from the context
        /// </summary>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <param name="entity">Entity</param>
        public virtual void Detach<TEntity>(TEntity entity) where TEntity : class
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var entityEntry = this.Entry(entity);
            if (entityEntry == null)
                return;

            //set the entity is not being tracked by the context
            entityEntry.State = EntityState.Detached;
        }

        public List<dynamic> GetMultipleResultSet(string Query, List<Type> collection, params object[] parameters)
        {
            int resultCounter = 0;
            try
            {
                var command = this.Database.GetDbConnection().CreateCommand();
                command.CommandText = Query;
                command.CommandType = CommandType.Text;
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }
                this.Database.OpenConnection();
                List<dynamic> x = new List<dynamic>();

                using (var reader = command.ExecuteReader())
                {
                    do
                    {
                        Type type = collection[resultCounter];
                        dynamic results = null;
                        if (type.IsClass)
                            results = createList(type);
                        var columns = Enumerable.Range(0, reader.FieldCount).Select(reader.GetName).ToList();
                        while (reader.Read())
                        {
                            var record = (IDataRecord)reader;
                            if (type.IsClass)
                            {

                                var obj = Materialize(record, type, columns);
                                results.Add(obj);

                            }
                            else
                            {
                                results = reader[0];
                            }
                        }
                        x.Add(results);

                        resultCounter++;

                    } while (!reader.IsClosed && reader.NextResult());
                }
                return x;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //_DatabaseContext.Database.Connection.Close();
            }
        }

        private IList createList(Type myType)
        {
            Type genericListType = typeof(List<>).MakeGenericType(myType);
            return (IList)Activator.CreateInstance(genericListType);
        }

        private dynamic Materialize(IDataRecord record, Type type, List<string> cols)
        {
            var t = Activator.CreateInstance(type);
            foreach (var prop in type.GetProperties())
            {
                // 1). If entity reference, bypass it.
                if (prop.PropertyType.Namespace == type.Namespace)
                {
                    continue;
                }
                // 2). If collection, bypass it.
                if (prop.PropertyType != typeof(string) && typeof(IEnumerable).IsAssignableFrom(prop.PropertyType))
                {
                    continue;
                }
                // 3). If property is NotMapped, bypass it.
                if (!cols.Exists(x => x.ToLower() == prop.Name.ToLower()))
                {
                    continue;
                }

                var dbValue = record[prop.Name];
                if (dbValue is DBNull) continue;
                if (prop.PropertyType.IsConstructedGenericType &&
                    prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    var baseType = prop.PropertyType.GetGenericArguments()[0];
                    var baseValue = Convert.ChangeType(dbValue, baseType);
                    var value = Activator.CreateInstance(prop.PropertyType, baseValue);
                    prop.SetValue(t, value);
                }
                else
                {
                    var value = Convert.ChangeType(dbValue, prop.PropertyType);
                    prop.SetValue(t, value);
                }
            }
            return t;
        }
        #endregion
    }
}
